using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Factories;
using Azure2Pulumi.Importer.Interfaces.Services;
using Azure2Pulumi.Importer.Models;


namespace Azure2Pulumi.Importer.Services
{
    public class ImportDataGenerator : IImportDataGenerator
    {
        private readonly PulumiResourceFactory _factory;

        public ImportDataGenerator()
        {
            _factory = new PulumiResourceFactory();
        }

        public IEnumerable<Result<PulumiResource>> GeneratePulumiResources(List<AzureResource> azureResources, Context context)
        {
            var resources = new List<Result<PulumiResource>>();
            foreach (var resource in azureResources)
            {
                resources.Add(_factory.Create(resource, context));
            }

            return resources;
        }
    }
}
