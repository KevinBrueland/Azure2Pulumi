using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.offazure.SqlDiscoverySiteDataSourceController
{
	//This class has been auto-generated by voodoo and magic.
	internal class SqlDiscoverySiteDataSourceController : ImportResource
	{
		public override string AzureResourceType => "Microsoft.OffAzure/masterSites/sqlSites/discoverySiteDataSources";
		protected override string PulumiResourceType => "azure-native:offazure:SqlDiscoverySiteDataSourceController";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OffAzure/masterSites/{siteName}/sqlSites/{sqlSiteName}/discoverySiteDataSources/{discoverySiteDataSourceName}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				var parentNames = PulumiResourceHelper.GetParentNames(azureResource.DependsOn);
				var importId = $"/subscriptions/{context.SubscriptionId}/resourceGroups/{context.ResourceGroupName}/providers/Microsoft.OffAzure/masterSites/{parentNames[0]}/sqlSites/{parentNames[1]}/discoverySiteDataSources/{importResourceName}";
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