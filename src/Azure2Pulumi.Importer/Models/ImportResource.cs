using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Helpers;

namespace Azure2Pulumi.Importer.Models
{
    public abstract class ImportResource
    {
        private static Dictionary<string, int> IdenticalNameLookup { get; set; } = new Dictionary<string, int>();

        public virtual Result<PulumiResource> Create(AzureResource azureResource, Context context)
        {
            var pulumiResource = new PulumiResource();
            pulumiResource.Name = PulumiResourceHelper.GetNameIfNested(azureResource.Name);
            pulumiResource.Type = PulumiResourceType;
            pulumiResource.Version = context.IncludeApiVersion ? azureResource.ApiVersion : null;
            pulumiResource.LogicalName = GetUniqueLogicalName(pulumiResource.Name);

            //If the resource has a resource id, use this
            var message = string.Empty;
            if (azureResource.Id != null)
            {
                pulumiResource.Id = azureResource.Id;
                return ResultBuilder.Success(pulumiResource);
            }

            //If not, try to build the resource id
            var createdImportIdResult = BuildPulumiImportId(azureResource, context);
            if (createdImportIdResult.IsSuccess)
            {
                pulumiResource.Id = createdImportIdResult.Content;
                return ResultBuilder.Success(pulumiResource);
            }

            //If that doesnt work, provide the default id template so it can be manually built
            pulumiResource.Id = DefaultTemplateResourceId;
            return ResultBuilder.Success(pulumiResource, createdImportIdResult.Message);
        }

        //If the resource has the same name as another resource, we need to give it a unique logical name for the pulumi import
        private string GetUniqueLogicalName(string name)
        {
            if (IdenticalNameLookup.ContainsKey(name))
            {
                var currentValue = IdenticalNameLookup[name];
                IdenticalNameLookup[name] = ++currentValue;
                return $"{name}_{IdenticalNameLookup[name]}";
            }

            IdenticalNameLookup.TryAdd(name, 0);
            return name;
        }

        //Used to identify the fetched azure resource from a template
        public abstract string AzureResourceType { get; }

        //What we map the resource type to in order for Pulumi to understand it
        protected abstract string PulumiResourceType { get; }

        //The Default template resource id from pulumi import, used as a fallback
        protected abstract string DefaultTemplateResourceId { get; }

        //Build the import id that pulumi needs
        protected abstract Result<string> BuildPulumiImportId(AzureResource azureResource, Context context);
    }
}
