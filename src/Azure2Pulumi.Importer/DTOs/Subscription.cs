namespace Azure2Pulumi.Importer.DTOs
{
    public class Subscription
    {
        public string? EnvironmentName { get; set; }
        public string? HomeTenantId { get; set; }
        public string? Id { get; set; }
        public bool IsDefault { get; set; }
        public string? Name { get; set; }
        public string? State { get; set; }
        public string? TenantId { get; set; }
    }
}
