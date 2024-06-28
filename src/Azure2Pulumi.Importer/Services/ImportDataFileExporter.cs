using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.Interfaces.Services;
using Azure2Pulumi.Importer.Models;
using System.Text.Json;

namespace Azure2Pulumi.Importer.Services
{
    public class ImportDataFileExporter : IImportDataFileExporter
    {
        public Result<string> WriteImportDataToSingleFile(PulumiImportData importData, string? filepath = null)
        {
            if (filepath == null)
                filepath = AppDomain.CurrentDomain.BaseDirectory;

            var fileName = $"{importData.ResourceGroupName}-import.json";
            try
            {
                var json = JsonSerializer.Serialize(new { resources = importData.Resources }, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                });
                File.WriteAllText(Path.Combine(filepath, fileName), json);

                return ResultBuilder.Success(Path.Combine(filepath, fileName));
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<string>($"Something went wrong when serializing pulumi resources. Ex: {ex}");
            }
        }

        public Result<string> WriteImportDataToMultipleFiles(List<IGrouping<string, PulumiResource>> importData, string resourceGroupName, string? filepath = null)
        {
            if (filepath == null)
                filepath = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                var dir = Directory.CreateDirectory(Path.Combine(filepath, resourceGroupName + "-import"));
                for (int i = 0; i < importData.Count; i++)
                {
                    var fileName = $"{importData[i].Key}-import.json";

                    var json = JsonSerializer.Serialize(new { resources = importData[i].ToList() }, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                    }); ;
                    File.WriteAllText(Path.Combine(dir.FullName, fileName), json);
                }

                return ResultBuilder.Success(Path.Combine(dir.FullName));
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<string>($"Unable to create import files for resource group {resourceGroupName}. EX: {ex}");
            }
        }

        public Result<string> WriteReportToFile(List<string> errors, string resourceGroupName, string? filepath = null)
        {
            if (filepath == null)
                filepath = AppDomain.CurrentDomain.BaseDirectory;

            var fileName = $"{resourceGroupName}-import-report.json";
            try
            {
                var json = JsonSerializer.Serialize(errors, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                });
                File.WriteAllText(Path.Combine(filepath, fileName), json);

                return ResultBuilder.Success(Path.Combine(filepath, fileName));
            }
            catch (Exception ex)
            {
                return ResultBuilder.Failure<string>($"Something went wrong when serializing pulumi resources. Ex: {ex}");
            }
        }
    }
}


