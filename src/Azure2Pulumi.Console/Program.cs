using Azure2Pulumi.Console.ColorSchemes;
using Azure2Pulumi.Console.Helpers;
using Azure2Pulumi.Console.Plumbing;
using Microsoft.Extensions.DependencyInjection;


namespace Azure2Pulumi.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = IoCProvider.CreateServices();

            var colorSchemeFactory = services.GetRequiredService<ColorSchemeFactory>();

            var app = services.GetRequiredService<Application>();

            var runArgs = Environment.GetCommandLineArgs();   
            
            var selectedColorScheme = CommandLineHelper.GetCommandLineArg(runArgs, "--color-scheme", "default");

            var colorScheme = colorSchemeFactory.Create(selectedColorScheme);

            app.Run(colorScheme);
        }

    }
}
