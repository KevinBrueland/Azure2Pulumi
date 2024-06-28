using Azure2Pulumi.Console.ColorSchemes;
using Azure2Pulumi.Console.Constants;
using Azure2Pulumi.Importer.Models;
using Spectre.Console;

namespace Azure2Pulumi.Console.Helpers
{

    public class ConsoleHelper
    {
        private ColorScheme _colorScheme = new DarkColorScheme();

        public void SetColorScheme(ColorScheme colorScheme)
        {
            if(colorScheme != null)
                _colorScheme = colorScheme;
        }
        public void Info(string message)
        {
            AnsiConsole.MarkupInterpolated($"[bold {_colorScheme.Info.ToMarkup()}]{message}[/]\n");
        }

        public void Warning(string message)
        {
            AnsiConsole.MarkupInterpolated($"[bold {_colorScheme.Warning.ToMarkup()}]{message}[/]\n");
        }

        public void Error(string message)
        {
            AnsiConsole.MarkupInterpolated($"[bold {_colorScheme.Error.ToMarkup()}]{message}[/]\n");
        }

        public void Success(string message)
        {
            AnsiConsole.MarkupInterpolated($"[bold {_colorScheme.Success.ToMarkup()}]{message}[/]\n");
        }

        public void Status(string message)
        {
            AnsiConsole.MarkupInterpolated($"[bold {_colorScheme.Status.ToMarkup()}]{message}[/]\n");
        }

        public void DisplayLogo()
        {
            AnsiConsole.Write(new FigletText("Azure2Pulumi")
                       .LeftJustified()
                       .Color(_colorScheme.Logo));
                ;

            AnsiConsole.Write(new FigletText("Import Tool \n")
                       .LeftJustified()
                       .Color(_colorScheme.Logo));
        }

        public string PromptActiveSubscription(List<string> possibleValues)
        {
            if (possibleValues == null)
                return string.Empty;

            var subscription = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title($"[{_colorScheme.PromptTitle.ToMarkup()}]Which Azure subscription contains the resource group you would like to import?[/]")
                        .PageSize(10)
                        .MoreChoicesText($"[{_colorScheme.MoreChoiceText.ToMarkup()}](Move up and down to show more options)[/]")
                        .AddChoices(possibleValues)
                        .HighlightStyle(new Style(_colorScheme.SelectedHighlightText)));

            return subscription;
        }

        public string PromptLoginAlternatives()
        {
            var loginChoices = new List<string>() { LoginAlternatives.Login, LoginAlternatives.LoginWithSpecificTenant };

            var selectedLoginAlternative = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title($"[{_colorScheme.PromptTitle.ToMarkup()}]Please select a login alternative. This will open a browser and prompt you to login with Azure[/]")
                        .PageSize(10)
                        .MoreChoicesText($"[{_colorScheme.MoreChoiceText.ToMarkup()}](Move up and down to show more options)[/]")
                        .AddChoices(loginChoices)
                        .HighlightStyle(new Style(_colorScheme.SelectedHighlightText)));

            return selectedLoginAlternative;
        }

        public string PromptTenantId()
        {
            var tenantId = AnsiConsole.Prompt(
                        new TextPrompt<string>("[yellow]Enter the tenant id: [/]")
                        .PromptStyle("grey85")
                        .ValidationErrorMessage("[red]Please enter a valid tenant id[/]")
                        .Validate(tenantId =>
                        {
                            return !string.IsNullOrEmpty(tenantId) && Guid.TryParse(tenantId, out _);
                        }));

            return tenantId;
        }

        public string PromptResourceGroupToImport(List<string> possibleValues)
        {
            var subscription = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title($"[{_colorScheme.PromptTitle.ToMarkup()}]Which resource group would you like to create an import for?[/]")
                        .PageSize(20)
                        .MoreChoicesText($"[{_colorScheme.MoreChoiceText.ToMarkup()}](Move up and down to show more options)[/]")
                        .AddChoices(possibleValues)
                        .HighlightStyle(new Style(_colorScheme.SelectedHighlightText)));

            return subscription;
        }

        public string PromptImportFileExportMode()
        {
            var exportChoices = new List<string>() { FileExportMode.SingleFile, FileExportMode.MultipleFiles };

            var chosenValue = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title($"[{_colorScheme.PromptTitle.ToMarkup()}]How would you like to export the import file(s)?[/]")
                        .PageSize(5)
                        .MoreChoicesText($"[{_colorScheme.MoreChoiceText.ToMarkup()}](Move up and down to show more options)[/]")
                        .AddChoices(exportChoices)
                        .HighlightStyle(new Style(_colorScheme.SelectedHighlightText)));

            return chosenValue;
        }

        public string PromptResourceImportMode()
        {
            var resourceSelectionChoices = new List<string>() { ResourceSelection.All, ResourceSelection.Selected };

            var chosenValue = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title($"[{_colorScheme.PromptTitle.ToMarkup()}]Do you want to create an import for all resources or only selected resources?[/]")
                        .PageSize(5)
                        .MoreChoicesText($"[{_colorScheme.MoreChoiceText.ToMarkup()}](Move up and down to show more options)[/]")
                        .AddChoices(resourceSelectionChoices)
                        .HighlightStyle(new Style(_colorScheme.SelectedHighlightText)));


            return chosenValue;
        }

        public List<string> PromptSelectedResourceToImport(Dictionary<string, List<PulumiResource>> availableResources)
        {
            var selectableResourceMultiPrompt = new MultiSelectionPrompt<string>()
                                                    .Title($"[{_colorScheme.PromptTitle.ToMarkup()}]Select resources to import (Type | Name | Resource Id)[/]")
                                                    .PageSize(30)
                                                    .MoreChoicesText($"[{_colorScheme.MoreChoiceText.ToMarkup()}](Move up and down to show more resources)[/]")
                                                    .InstructionsText(
                                                        $"[{_colorScheme.MoreChoiceText.ToMarkup()}](Press [{_colorScheme.InstructionSelect.ToMarkup()}]<space>[/] to add or remove resources, [{_colorScheme.InstructionAccept.ToMarkup()}]<enter>[/] to accept selection)[/]")
                                                    .HighlightStyle(new Style(_colorScheme.SelectedHighlightText));

            foreach (var resource in availableResources)
            { 
                selectableResourceMultiPrompt.AddChoiceGroup(resource.Key, resource.Value.OrderBy(x => x.Type).Select(x => x.Type.Split(":").Last() + " | " + x.Name + " | " + ResourceIdHelper.ShortenResourceId(x.Id)).ToList());
            }

            return AnsiConsole.Prompt(selectableResourceMultiPrompt);
        }
    }
}

