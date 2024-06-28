
namespace Azure2Pulumi.Importer.Models
{
    public class PulumiImportData
    {
        public PulumiImportData(string resourceGroupName)
        {
            ResourceGroupName = resourceGroupName;
        }
        public string ResourceGroupName { get; private set; }
        public List<PulumiResource> Resources { get; set; } = new List<PulumiResource>();
    }
}
