
using System.Text.Json.Serialization;
using Azure2Pulumi.Importer.Helpers;

namespace Azure2Pulumi.Importer.DTOs
{
    public class AzureResource : IEquatable<AzureResource?>
    {
        [JsonConverter(typeof(TrimmingConverter))]
        public string? Type { get; set; }
        [JsonConverter(typeof(TrimmingConverter))]
        public string? ApiVersion { get; set; }
        [JsonConverter(typeof(TrimmingConverter))]
        public string? Name { get; set; }
        public string[]? DependsOn { get; set; }
        [JsonConverter(typeof(TrimmingConverter))]
        public string? Id { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AzureResource);
        }

        public bool Equals(AzureResource? other)
        {
            return other is not null &&
                   Type == other.Type &&
                   ApiVersion == other.ApiVersion &&
                   Name == other.Name &&
                   EqualityComparer<string[]?>.Default.Equals(DependsOn, other.DependsOn) &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, ApiVersion, Name, DependsOn, Id);
        }

        public static bool operator ==(AzureResource? left, AzureResource? right)
        {
            return EqualityComparer<AzureResource>.Default.Equals(left, right);
        }

        public static bool operator !=(AzureResource? left, AzureResource? right)
        {
            return !(left == right);
        }
    }
}
