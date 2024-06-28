using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.costmanagement.MarkupRule
{
	//This class has been auto-generated by voodoo and magic.
	internal class MarkupRule : ImportResource
	{
		public override string AzureResourceType => "Microsoft.CostManagement/markupRules";
		protected override string PulumiResourceType => "azure-native:costmanagement:MarkupRule";
		protected override string DefaultTemplateResourceId => "/providers/Microsoft.Billing/billingAccounts/{billingAccountId}/billingProfiles/{billingProfileId}/providers/Microsoft.CostManagement/markupRules/{name}";

        protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
        {
            var errorMessage = $"Unable to build ResourceId for resource {azureResource.Name} of type {AzureResourceType} | {PulumiResourceType}. Please find this resource in the import json and manually build the id based on the provided template.";
            return ResultBuilder.Failure<string>(errorMessage);
        }
    }
}