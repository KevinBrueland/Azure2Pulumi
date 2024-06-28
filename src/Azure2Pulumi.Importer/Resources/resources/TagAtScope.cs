using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.resources.TagAtScope
{
	//This class has been auto-generated by voodoo and magic.
	internal class TagAtScope : ImportResource
	{
		public override string AzureResourceType => "Microsoft.Resources/tags";
		protected override string PulumiResourceType => "azure-native:resources:TagAtScope";
		protected override string DefaultTemplateResourceId => "/{scope}/providers/Microsoft.Resources/tags/default";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				
				var importId = $"/{importResourceName}/providers/Microsoft.Resources/tags/default";
				return ResultBuilder.Success(importId);
			}
			catch (Exception)
			{
				var errorMessage = $"Unable to build ResourceId for resource {azureResource.Name} of type {AzureResourceType} | {PulumiResourceType}. Please find this resource in the import json and manually build the id based on the provided template.";
				return ResultBuilder.Failure<string>(errorMessage);
			}
		}
	}
}