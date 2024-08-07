using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.insights.GuestDiagnosticsSetting
{
	//This class has been auto-generated by voodoo and magic.
	internal class GuestDiagnosticsSetting : ImportResource
	{
		public override string AzureResourceType => "microsoft.insights/guestDiagnosticSettings";
		protected override string PulumiResourceType => "azure-native:insights:GuestDiagnosticsSetting";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}/providers/microsoft.insights/guestDiagnosticSettings/{diagnosticSettingsName}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				
				var importId = $"/subscriptions/{context.SubscriptionId}/resourcegroups/{context.ResourceGroupName}/providers/microsoft.insights/guestDiagnosticSettings/{importResourceName}";
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
