
using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.Interfaces.Factories
{
    public interface IPulumiResourceFactory
    {
        Result<PulumiResource> Create(AzureResource resource, Context context);
    }
}
