using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.managedservices.RegistrationAssignment
{
	//This class has been auto-generated by voodoo and magic.
	internal class RegistrationAssignment : ImportResource
	{
		public override string AzureResourceType => "Microsoft.ManagedServices/registrationAssignments";
		protected override string PulumiResourceType => "azure-native:managedservices:RegistrationAssignment";
		protected override string DefaultTemplateResourceId => "/{scope}/providers/Microsoft.ManagedServices/registrationAssignments/{registrationAssignmentId}";

        protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
        {
            var errorMessage = $"Unable to build ResourceId for resource {azureResource.Name} of type {AzureResourceType} | {PulumiResourceType}. Please find this resource in the import json and manually build the id based on the provided template.";
            return ResultBuilder.Failure<string>(errorMessage);
        }
    }
}
