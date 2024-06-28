
namespace Azure2Pulumi.Console.Helpers
{
    public class ResourceIdHelper
    {
        public static string ShortenResourceId(string resourceId)
        {
            var parts = resourceId.Split("/providers/");
            return parts.Length > 1 ? parts.Last() : string.Empty;
        }

        public static string ExpandResourceId(string shortenedResourceId, string subscriptionId, string resourceGroupName)
        {
            return $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/{shortenedResourceId}";
        }
        
    }
}
