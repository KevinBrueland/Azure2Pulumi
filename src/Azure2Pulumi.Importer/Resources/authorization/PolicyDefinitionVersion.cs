using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.authorization.PolicyDefinitionVersion
{
	//This class has been auto-generated by voodoo and magic.
	internal class PolicyDefinitionVersion : ImportResource
	{
		public override string AzureResourceType => "Microsoft.Authorization/policyDefinitions/versions";
		protected override string PulumiResourceType => "azure-native:authorization:PolicyDefinitionVersion";
		protected override string DefaultTemplateResourceId => "/subscriptions/{subscriptionId}/providers/Microsoft.Authorization/policyDefinitions/{policyDefinitionName}/versions/{policyDefinitionVersion}";

		protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
		{
			try
			{
			    var importResourceName = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
				var parentNames = PulumiResourceHelper.GetParentNames(azureResource.DependsOn);
				var importId = $"/subscriptions/{context.SubscriptionId}/providers/Microsoft.Authorization/policyDefinitions/{parentNames[0]}/versions/{importResourceName}";
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
