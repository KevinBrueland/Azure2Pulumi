using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.recoveryservices.ProtectedItem
{
	//This class has been auto-generated by voodoo and magic.
	internal class ProtectedItem : ImportResource
	{
		public override string AzureResourceType => "Microsoft.RecoveryServices/vaults/backupFabrics/protectionContainers/protectedItems";
		protected override string PulumiResourceType => "azure-native:recoveryservices:ProtectedItem";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{vaultName}/backupFabrics/{fabricName}/protectionContainers/{containerName}/protectedItems/{protectedItemName}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				var parentNames = PulumiResourceHelper.GetParentNames(azureResource.DependsOn);
				var importId = $"/subscriptions/{context.SubscriptionId}/resourceGroups/{context.ResourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{parentNames[0]}/backupFabrics/{parentNames[1]}/protectionContainers/{parentNames[2]}/protectedItems/{importResourceName}";
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
