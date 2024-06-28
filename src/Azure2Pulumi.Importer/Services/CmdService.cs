using Azure2Pulumi.Importer.Interfaces.Services;
using System.Diagnostics;

namespace Azure2Pulumi.Importer.Services
{
    public class CmdService : ICmdService
    {
        public string ExecuteCommand(string command)
        {
            // '/c' tells cmd that we want it to execute the command that follows, and then exit.
            var processStartInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            var process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            // Get the output into a string
            var result = process.StandardOutput.ReadToEnd();

            return result;
        }
    }
}
