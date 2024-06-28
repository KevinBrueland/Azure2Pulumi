
using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;

namespace Azure2Pulumi.Importer.Models
{
    internal class NotImplementedResource : ImportResource
    {
        public override string AzureResourceType => "Not implemented";
        protected override string PulumiResourceType => "Pulumi resource type not implemented for this resource yet";

        protected override string DefaultTemplateResourceId => "Not implemented";

        public override Result<PulumiResource> Create(AzureResource azureResource, Context context)
        {
            var importIdErrorMessage = BuildPulumiImportId(azureResource, context).Message;
            return ResultBuilder.Failure<PulumiResource>(importIdErrorMessage);

        }

        protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
        {
            return ResultBuilder.Failure<string>($"Resource name: {azureResource.Name} | No resource implemented for the Azure Resource Type {azureResource.Type}.");
        }

    }
}
