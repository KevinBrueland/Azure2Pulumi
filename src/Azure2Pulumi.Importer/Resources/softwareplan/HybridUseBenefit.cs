using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.softwareplan.HybridUseBenefit
{
	//This class has been auto-generated by voodoo and magic.
	internal class HybridUseBenefit : ImportResource
	{
		public override string AzureResourceType => "Microsoft.SoftwarePlan/hybridUseBenefits";
		protected override string PulumiResourceType => "azure-native:softwareplan:HybridUseBenefit";
		protected override string DefaultTemplateResourceId => "/{scope}/providers/Microsoft.SoftwarePlan/hybridUseBenefits/{planId}";

        protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
        {
            var errorMessage = $"Unable to build ResourceId for resource {azureResource.Name} of type {AzureResourceType} | {PulumiResourceType}. Please find this resource in the import json and manually build the id based on the provided template.";
            return ResultBuilder.Failure<string>(errorMessage);
        }
    }
}
