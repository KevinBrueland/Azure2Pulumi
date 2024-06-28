
namespace Azure2Pulumi.Importer.Constants
{
    public static class AzCommands
    {
        public static string ListAccounts => "az account list";
        public static string ShowAccount => "az account show";
        public static string Login => "az login";
        public static string ListResourceGroups => "az group list";
        public static string ShowResourceGroup(string resourceGroupName) => $"az group show --name \"{resourceGroupName}\"";

        public static string ListResourcesInResourceGroup(string resourceGroupName) => $"az resource list --resource-group \"{resourceGroupName}\"";

        public static string ExportResourceGroupTemplate(string resourceGroupName) => $"az group export --name \"{resourceGroupName}\" --skip-all-params";

        public static string LoginWithTenant(string tenantId) => $"az login --tenant {tenantId}";

        public static string SetSubscription(string subscriptionName) => $"az account set --subscription \"{subscriptionName}\"";



    }
}
