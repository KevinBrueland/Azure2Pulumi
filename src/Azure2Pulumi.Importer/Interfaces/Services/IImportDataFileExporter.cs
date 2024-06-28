using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.Models;

namespace Azure2Pulumi.Importer.Interfaces.Services
{
    public interface IImportDataFileExporter
    {
        Result<string> WriteImportDataToSingleFile(PulumiImportData importData, string? filepath = null);

        Result<string> WriteImportDataToMultipleFiles(List<IGrouping<string, PulumiResource>> importData, string resourceGroupName, string? filepath = null);

        Result<string> WriteReportToFile(List<string> errors, string resourceGroupName, string? filepath = null);
    }
}
