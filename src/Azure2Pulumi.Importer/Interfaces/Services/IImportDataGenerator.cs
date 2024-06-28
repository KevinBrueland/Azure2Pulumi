using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.Interfaces.Services
{
    public interface IImportDataGenerator
    {
        IEnumerable<Result<PulumiResource>> GeneratePulumiResources(List<AzureResource> azureResources, Context context);
    }
}
