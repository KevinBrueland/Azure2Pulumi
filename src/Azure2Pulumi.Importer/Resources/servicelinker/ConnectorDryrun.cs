using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.servicelinker.ConnectorDryrun
{
	//This class has been auto-generated by voodoo and magic.
	internal class ConnectorDryrun : ImportResource
	{
		public override string AzureResourceType => "Microsoft.ServiceLinker/locations/dryruns";
		protected override string PulumiResourceType => "azure-native:servicelinker:ConnectorDryrun";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}/providers/Microsoft.ServiceLinker/locations/{location}/dryruns/{dryrunName}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				var parentNames = PulumiResourceHelper.GetParentNames(azureResource.DependsOn);
				var importId = $"/subscriptions/{context.SubscriptionId}/resourcegroups/{context.ResourceGroupName}/providers/Microsoft.ServiceLinker/locations/{parentNames[0]}/dryruns/{importResourceName}";
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