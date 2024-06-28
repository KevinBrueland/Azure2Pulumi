using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.marketplace.PrivateStoreCollectionOffer
{
	//This class has been auto-generated by voodoo and magic.
	internal class PrivateStoreCollectionOffer : ImportResource
	{
		public override string AzureResourceType => "Microsoft.Marketplace/privateStores/collections/offers";
		protected override string PulumiResourceType => "azure-native:marketplace:PrivateStoreCollectionOffer";
		protected override string DefaultTemplateResourceId => "/providers/Microsoft.Marketplace/privateStores/{privateStoreId}/collections/{collectionId}/offers/{offerId}";

        protected override Result<string> BuildPulumiImportId(AzureResource azureResource, Context context)
        {
            var errorMessage = $"Unable to build ResourceId for resource {azureResource.Name} of type {AzureResourceType} | {PulumiResourceType}. Please find this resource in the import json and manually build the id based on the provided template.";
            return ResultBuilder.Failure<string>(errorMessage);
        }
    }
}
