using Azure2Pulumi.Importer.Helpers;
using System.Text.Json.Serialization;

namespace Azure2Pulumi.Importer.Models
{

    public class PulumiResource
    {
        [JsonConverter(typeof(TrimmingConverter))]
        public string Name { get; set; }
        [JsonConverter(typeof(TrimmingConverter))]
        public string Type { get; set; }
        [JsonConverter(typeof(TrimmingConverter))]
        public string Id { get; set; }
        [JsonConverter(typeof(TrimmingConverter))]
        public string Version { get; set; }
        [JsonConverter(typeof(TrimmingConverter))]
        public string LogicalName { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PulumiResource resource &&
                   Name == resource.Name &&
                   Type == resource.Type &&
                   Id == resource.Id &&
                   Version == resource.Version &&
                   LogicalName == resource.LogicalName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Id, LogicalName);
        }

        public static bool operator ==(PulumiResource? left, PulumiResource? right)
        {
            return EqualityComparer<PulumiResource>.Default.Equals(left, right);
        }

        public static bool operator !=(PulumiResource? left, PulumiResource? right)
        {
            return !(left == right);
        }
    }
}
