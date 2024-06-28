using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.desktopvirtualization.PrivateEndpointConnectionByWorkspace
{
	//This class has been auto-generated by voodoo and magic.
	internal class PrivateEndpointConnectionByWorkspace : ImportResource
	{
		public override string AzureResourceType => "Microsoft.DesktopVirtualization/workspaces/privateEndpointConnections";
		protected override string PulumiResourceType => "azure-native:desktopvirtualization:PrivateEndpointConnectionByWorkspace";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.DesktopVirtualization/workspaces/{workspaceName}/privateEndpointConnections/{privateEndpointConnectionName}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				var parentNames = PulumiResourceHelper.GetParentNames(azureResource.DependsOn);
				var importId = $"/subscriptions/{context.SubscriptionId}/resourceGroups/{context.ResourceGroupName}/providers/Microsoft.DesktopVirtualization/workspaces/{parentNames[0]}/privateEndpointConnections/{importResourceName}";
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
