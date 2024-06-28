using Microsoft.Extensions.DependencyInjection;
using Azure2Pulumi.Importer.Extensions;
using Azure2Pulumi.Console.ColorSchemes;

namespace Azure2Pulumi.Console.Plumbing
{
    public class IoCProvider
    {
        public static ServiceProvider CreateServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<Application>()
                .AddSingleton<ColorSchemeFactory>()
                .AddAzure2Pulumi()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
  
}
