using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.networkcloud.KubernetesCluster
{
	//This class has been auto-generated by voodoo and magic.
	internal class KubernetesCluster : ImportResource
	{
		public override string AzureResourceType => "Microsoft.NetworkCloud/kubernetesClusters";
		protected override string PulumiResourceType => "azure-native:networkcloud:KubernetesCluster";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.NetworkCloud/kubernetesClusters/{kubernetesClusterName}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				
				var importId = $"/subscriptions/{context.SubscriptionId}/resourceGroups/{context.ResourceGroupName}/providers/Microsoft.NetworkCloud/kubernetesClusters/{importResourceName}";
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
