
namespace Azure2Pulumi.Console.Helpers
{
    public class CommandLineHelper
    {
        public static string GetCommandLineArg(string[] args, string targetArgName, string defaultValueIFNotFound)
        {
            var value = defaultValueIFNotFound;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals(targetArgName, StringComparison.InvariantCultureIgnoreCase) && i + 1 < args.Length)
                {
                    value = args[i + 1];
                    break;
                }
            }

            return value;
        }
    }
}
