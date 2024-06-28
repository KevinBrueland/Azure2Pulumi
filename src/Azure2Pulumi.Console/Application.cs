using Azure2Pulumi.Console.ColorSchemes;
using Azure2Pulumi.Console.Constants;
using Azure2Pulumi.Console.Helpers;
using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Interfaces.Services;
using Azure2Pulumi.Importer.Models;
using Spectre.Console;

namespace Azure2Pulumi.Console
{
    public class Application
    {
        private readonly IAzureResourceService _azureResourceService;
        private readonly IImportDataGenerator _pulumiDataGenerator;
        private readonly IImportDataFileExporter _fileExporter;
        private readonly ConsoleHelper _consoleHelper;

        public Application(IAzureResourceService azureResourceService, IImportDataGenerator pulumiDataGenerator, IImportDataFileExporter fileExporter)
        {
            _azureResourceService = azureResourceService;
            _pulumiDataGenerator = pulumiDataGenerator;
            _fileExporter = fileExporter;
            _consoleHelper = new ConsoleHelper();
           
        }
        public void Run(ColorScheme? colorScheme)
        {
            _consoleHelper.SetColorScheme(colorScheme);
            _consoleHelper.DisplayLogo();

            AnsiConsole.WriteLine("");

            //Get all possible subscriptions available to the user
            var getPossibleSubscriptionsResult = _azureResourceService.GetAllAvailableSubscriptionsByName();
            if (!getPossibleSubscriptionsResult.IsSuccess)
            {
                //If no subscriptions are found, prompt the user to login
                _consoleHelper.Warning(getPossibleSubscriptionsResult.Message);
                var loginResult = new Result<bool>();

                var loginAlternative = _consoleHelper.PromptLoginAlternatives();
                switch (loginAlternative)
                {
                    case LoginAlternatives.Login:
                        loginResult = _azureResourceService.LoginIfNot();
                        break;
                    case LoginAlternatives.LoginWithSpecificTenant:
                        var tenantId = _consoleHelper.PromptTenantId();
                        loginResult = _azureResourceService.LoginWithSpecificTenantIfNot(tenantId);
                        break;
                }

                if (!loginResult.IsSuccess)
                {
                    _consoleHelper.Warning(loginResult.Message);
                    Environment.Exit(0);
                }

                _consoleHelper.Success(loginResult.Message);

                getPossibleSubscriptionsResult = _azureResourceService.GetAllAvailableSubscriptionsByName();
            }

            //Ask which subscription to use
            var selectedActiveSubscription = _consoleHelper.PromptActiveSubscription(getPossibleSubscriptionsResult.Content);
            _azureResourceService.SetCurrentActiveSubscription(selectedActiveSubscription);
            var activeSubscription = _azureResourceService.GetActiveSubscriptionContext();
            if (activeSubscription.IsSuccess)
                _consoleHelper.Status($"Current active subscription context: {activeSubscription.Content.Name} ({activeSubscription.Content.Id}) \n");
            else
            {
                _consoleHelper.Error(activeSubscription.Message);
                Environment.Exit(0);
            }

            //Get all possible resource groups in the selected subscription
            var getPossibleResourceGroupsResult = _azureResourceService.GetAllAvailableResourceGroupsByName();
            if (!getPossibleResourceGroupsResult.IsSuccess)
            {
                _consoleHelper.Error(getPossibleResourceGroupsResult.Message);
                Environment.Exit(0);
            }

            //Ask which available resource group to import
            var selectedActiveResourceGroupName = _consoleHelper.PromptResourceGroupToImport(getPossibleResourceGroupsResult.Content);
            var azureResources = new List<AzureResource>();
            var resourceGroup = _azureResourceService.GetResourceGroup(selectedActiveResourceGroupName);
            if (resourceGroup.IsSuccess)
            {
                azureResources.Add(resourceGroup.Content);
                _consoleHelper.Status($"Resource group to import: {resourceGroup.Content.Name} \n");
            }
            else
            {
                _consoleHelper.Error(resourceGroup.Message);
                Environment.Exit(0);
            }

            var pulumiImportData = new PulumiImportData(selectedActiveResourceGroupName);
            var context = new Context { ResourceGroupName = selectedActiveResourceGroupName, SubscriptionId = activeSubscription.Content.Id };

            _consoleHelper.Info($"Trying to fetch resources in resources group...");
            var resourceGroupResources = _azureResourceService.GetAllResourcesInResourceGroup(resourceGroup.Content.Name);

            if (resourceGroupResources.IsSuccess)
            {
                azureResources.AddRange(resourceGroupResources.Content);
                _consoleHelper.Info($"Resource group resource count: {azureResources.Count()}");
            }

            _consoleHelper.Info($"Trying to fetch nested resources...(this could take a while if you have alot of resources)");
            var nestedResources = _azureResourceService.GetResourceGroupExportTemplate(resourceGroup.Content.Name);
            if (nestedResources.IsSuccess)
            {
                _consoleHelper.Info($"Nested resource count: {nestedResources.Content.Count()}");
                azureResources = azureResources.Union(nestedResources.Content).ToList();
                _consoleHelper.Info($"Total resources found: {azureResources.Count()}");
            }

            // Lets convert the azure resources to the format that pulumi expects for the import
            var pulumiResources = _pulumiDataGenerator.GeneratePulumiResources(azureResources, context).Distinct();
            var successResources = pulumiResources.Where(r => r.IsSuccess);
            var errors = pulumiResources.Where(r => !r.IsSuccess).Select(x => x.Message).ToList();

            var uniqueResource = new Dictionary<string, PulumiResource>(StringComparer.OrdinalIgnoreCase);

            //Resource Filter

            _consoleHelper.Info("Applying filter to remove all default nested resources that are automatically created with a parent...");

            foreach (var pulumiResource in successResources)
            {
                //Currently ignoring default resources that arent importable, as well as not-implemented types
                //These values should be optional values when starting an import
                if (!string.Equals(pulumiResource.Content.Name, "default", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(pulumiResource.Content.Name, "$default", StringComparison.OrdinalIgnoreCase))
                {
                    uniqueResource.TryAdd(pulumiResource.Content.Id, pulumiResource.Content);
                    if (!string.IsNullOrEmpty(pulumiResource.Message))
                        errors.Add(pulumiResource.Message);
                }
            }

            _consoleHelper.Info($"Total resource count after applying filters: {uniqueResource.Count} \n");
            var distinctImportResources = uniqueResource.Values.DistinctByMultiple(x => new { x.Type, x.Id }).ToList();

            //Import all or selected resources:
            var importResourceMode = _consoleHelper.PromptResourceImportMode();
            var importResources = new List<PulumiResource>();
            switch (importResourceMode)
            {
                case ResourceSelection.All:
                    {
                        importResources = distinctImportResources;
                        break;
                    }
                case ResourceSelection.Selected:
                    {
                        var selectableImportResources = distinctImportResources.GroupBy(x => x.Type.Split(":")[1]).ToDictionary(k => k.Key, v => v.ToList());
                        var selected = _consoleHelper.PromptSelectedResourceToImport(selectableImportResources);

                        var resourcesToKeep = selected.ToDictionary(k => ResourceIdHelper.ExpandResourceId(k.Split('|')[2].Trim(), context.SubscriptionId, context.ResourceGroupName), v => v.Split('|')[1].Trim());
                        foreach (var resource in distinctImportResources)
                        {
                            if (resourcesToKeep.ContainsKey(resource.Id))
                                importResources.Add(resource);
                        }
                        break;
                    }
                default:
                    break;
            }

            pulumiImportData.Resources.AddRange(importResources);

            //Print all selected import resources in single file or in separate files per module
            var exportMode = _consoleHelper.PromptImportFileExportMode();
            switch (exportMode)
            {
                case FileExportMode.SingleFile:
                    {
                        var singleFileImportResult = _fileExporter.WriteImportDataToSingleFile(pulumiImportData);
                        if (singleFileImportResult.IsSuccess)
                            _consoleHelper.Success($"Pulumi import file writted to file: {singleFileImportResult.Content}");
                        else
                        {
                            _consoleHelper.Error($"Unable to write pulumi import file. Reason: {singleFileImportResult.Message}");
                            Environment.Exit(0);
                        }
                        break;
                    }

                case FileExportMode.MultipleFiles:
                    {
                        var successResourcesGroupedByType = pulumiImportData.Resources.GroupBy(x => x.Type.Split(":")[1]).ToList();
                        var multipleFileImportResult = _fileExporter.WriteImportDataToMultipleFiles(successResourcesGroupedByType, pulumiImportData.ResourceGroupName);
                        if (multipleFileImportResult.IsSuccess)
                            _consoleHelper.Success($"Pulumi import files writted to folder: {multipleFileImportResult.Content}");
                        else
                        {
                            _consoleHelper.Error($"Unable to write pulumi import files. Reason: {multipleFileImportResult.Message}");
                            Environment.Exit(0);
                        }
                        break;
                    }
                default:
                    _fileExporter.WriteImportDataToSingleFile(pulumiImportData);
                    break;
            }

            if (errors.Any())
            {
                var errorResult = _fileExporter.WriteReportToFile(errors, pulumiImportData.ResourceGroupName);
                _consoleHelper.Warning("Please check the import report to see if there were any problems with the import");
                _consoleHelper.Warning($"The report file has been writted to: {errorResult.Content}");
            }

            _consoleHelper.Status("");
            _consoleHelper.Status("What's next?");
            _consoleHelper.Info("1. Create a new Pulumi stack to hold the resources you want to import");
            _consoleHelper.Info("2. In a command prompt, run the command 'pulumi import -f <importfile.json>' against the newly created stack");
            _consoleHelper.Info("3. Copy the generated code from the import command and paste it into your Pulumi stack.");
        }
    }
    
}
