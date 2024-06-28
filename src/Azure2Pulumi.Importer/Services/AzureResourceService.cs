using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.Constants;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Interfaces.Services;
using System.Text.Json;

namespace Azure2Pulumi.Importer.Services
{
    public class AzureResourceService : IAzureResourceService
    {
        private readonly ICmdService _cmdService;

        public AzureResourceService(ICmdService cmdService)
        {
            _cmdService = cmdService;
        }

        public Result<List<string>> GetAllAvailableSubscriptionsByName()
        {
            try
            {
                var subscriptionContext = _cmdService.ExecuteCommand(AzCommands.ListAccounts);

                var subscriptions = JsonSerializer.Deserialize<Subscription[]>(subscriptionContext, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                var availableSubscriptionNames = subscriptions.Select(s => s.Name).OrderBy(x => x).ToList();
                if (availableSubscriptionNames.Any())
                    return ResultBuilder.Success(availableSubscriptionNames);

                return ResultBuilder.Failure<List<string>>("No subscriptions found! You either do not have access to any subscriptions or you are not logged in with 'az login'.");
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<List<string>>($"Unable to convert response to Subscription. Ex: {ex}");
            }
        }

        public Result<bool> LoginIfNot()
        {
            var currentSubscription = _cmdService.ExecuteCommand(AzCommands.ShowAccount);

            if (string.IsNullOrEmpty(currentSubscription))
            {
                _cmdService.ExecuteCommand(AzCommands.Login);
                return ResultBuilder.Success(true, "Logged in!");
            }

            return ResultBuilder.Failure<bool>("Already logged in");
                
        }

        public Result<bool> LoginWithSpecificTenantIfNot(string tenantId)
        {
            var currentSubscription = _cmdService.ExecuteCommand(AzCommands.ShowAccount);

            if (string.IsNullOrEmpty(currentSubscription))
            {
                _cmdService.ExecuteCommand(AzCommands.LoginWithTenant(tenantId));
                return ResultBuilder.Success(true, "Logged in!");
            }

            return ResultBuilder.Failure<bool>("Already logged in");
        }

        public Result<string> SetCurrentActiveSubscription(string subscriptionName)
        {
            try
            {
                var subscriptionContext = _cmdService.ExecuteCommand(AzCommands.SetSubscription(subscriptionName));
                return ResultBuilder.Success(subscriptionName);
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<string>($"Unable to set the current subscription. Ex: {ex}");
            }
        }

        public Result<Subscription> GetActiveSubscriptionContext()
        {
            try
            {
                var subscriptionContext = _cmdService.ExecuteCommand(AzCommands.ShowAccount);

                var subscription = JsonSerializer.Deserialize<Subscription>(subscriptionContext, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                if (subscription != null)
                    return ResultBuilder.Success(subscription);

                return ResultBuilder.Failure<Subscription>($"Unable to fetch the current active subscription. Make sure you are logged in with 'az login'.");
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<Subscription>($"Unable to convert response to Subscription. Ex: {ex}");
            }
        }

        public Result<List<string>> GetAllAvailableResourceGroupsByName()
        {
            var subscriptionContext = _cmdService.ExecuteCommand(AzCommands.ListResourceGroups);

            if (string.IsNullOrEmpty(subscriptionContext))
                return ResultBuilder.Failure<List<string>>($"Unable to fetch any Resource groups. Make sure you have access to resource groups in the selected subscription. You might have to refresh your login.");

            try
            {
                var resourceGroups = JsonSerializer.Deserialize<AzureResource[]>(subscriptionContext, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                var availableresourceGroupNames = resourceGroups.Select(s => s.Name).OrderBy(x => x).ToList();
                return ResultBuilder.Success(availableresourceGroupNames);
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<List<string>>($"Unable to convert response to Subscription. Ex: {ex}");
            }
        }

        public Result<AzureResource> GetResourceGroup(string resourceGroupName)
        {
            var resourceGroupRaw = _cmdService.ExecuteCommand(AzCommands.ShowResourceGroup(resourceGroupName));

            if (string.IsNullOrEmpty(resourceGroupName))
                return ResultBuilder.Failure<AzureResource>($"Unable to fetch the resource group with name {resourceGroupName}. Make sure you are in the correct subscription context");

            try
            {
                var resourceGroup = JsonSerializer.Deserialize<AzureResource>(resourceGroupRaw, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                return ResultBuilder.Success(resourceGroup);
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<AzureResource>($"Unable to convert response to Resource. Ex: {ex}");
            }
        }

        public Result<AzureResource[]> GetAllResourcesInResourceGroup(string resourceGroupName)
        {
            var resourceGroupResourcesRaw = _cmdService.ExecuteCommand(AzCommands.ListResourcesInResourceGroup(resourceGroupName));
            if (string.IsNullOrEmpty(resourceGroupResourcesRaw))
                return ResultBuilder.Failure<AzureResource[]>($"Unable to fetch the any resources in the resource group {resourceGroupName}. Make sure you are in the correct subscription context.");

            try
            {
                var resourceGroup = JsonSerializer.Deserialize<AzureResource[]>(resourceGroupResourcesRaw, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                return ResultBuilder.Success(resourceGroup);
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<AzureResource[]>($"Unable to convert response to Resource. Ex: {ex}");
            }
        }

        public Result<AzureResource[]> GetResourceGroupExportTemplate(string resourceGroupName)
        {
            var resourceGroupTemplateRaw = _cmdService.ExecuteCommand(AzCommands.ExportResourceGroupTemplate(resourceGroupName));

            if (string.IsNullOrEmpty(resourceGroupTemplateRaw))
                return ResultBuilder.Failure<AzureResource[]>($"Unable to fetch the export template for the resource group {resourceGroupName}. Make sure you are in the correct subscription context.");

            try
            {
                var resourceGroupExportTemplate = JsonSerializer.Deserialize<Template>(resourceGroupTemplateRaw, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                return ResultBuilder.Success(resourceGroupExportTemplate.Resources);
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<AzureResource[]>($"Unable to convert response to Resource. Ex: {ex}");
            }
        }
    }
}
