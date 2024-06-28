using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.datamigration.File
{
	//This class has been auto-generated by voodoo and magic.
	internal class File : ImportResource
	{
		public override string AzureResourceType => "Microsoft.DataMigration/services/projects/files";
		protected override string PulumiResourceType => "azure-native:datamigration:File";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/resourceGroups/{groupName}/providers/Microsoft.DataMigration/services/{serviceName}/projects/{projectName}/files/{fileName}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				var parentNames = PulumiResourceHelper.GetParentNames(azureResource.DependsOn);
				var importId = $"/subscriptions/{context.SubscriptionId}/resourceGroups/{parentNames[0]}/providers/Microsoft.DataMigration/services/{parentNames[1]}/projects/{parentNames[2]}/files/{importResourceName}";
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
