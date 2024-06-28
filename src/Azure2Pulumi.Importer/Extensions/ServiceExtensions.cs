using Azure2Pulumi.Importer.Factories;
using Azure2Pulumi.Importer.Interfaces.Factories;
using Azure2Pulumi.Importer.Interfaces.Services;
using Azure2Pulumi.Importer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Azure2Pulumi.Importer.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAzure2Pulumi(this IServiceCollection services)
        {
            services.AddScoped<IAzureResourceService, AzureResourceService>();
            services.AddScoped<ICmdService, CmdService>();
            services.AddScoped<IImportDataFileExporter, ImportDataFileExporter>();
            services.AddScoped<IImportDataGenerator, ImportDataGenerator>();
            services.AddScoped<IPulumiResourceFactory, PulumiResourceFactory>();

            return services;
        }
    }
}
