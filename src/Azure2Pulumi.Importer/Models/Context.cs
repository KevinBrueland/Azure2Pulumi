
namespace Azure2Pulumi.Importer.Models
{
    public class Context
    {
        public string ResourceGroupName { get; set; }
        public string SubscriptionId { get; set; }

        public bool IncludeApiVersion { get; set; }

    }
}
