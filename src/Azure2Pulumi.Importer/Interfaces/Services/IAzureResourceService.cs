using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;

namespace Azure2Pulumi.Importer.Interfaces.Services
{
    public interface IAzureResourceService
    {
        Result<List<string>> GetAllAvailableSubscriptionsByName();
        Result<bool> LoginIfNot();
        Result<bool> LoginWithSpecificTenantIfNot(string tenantId);

        Result<string> SetCurrentActiveSubscription(string subscriptionName);

        Result<Subscription> GetActiveSubscriptionContext();

        Result<List<string>> GetAllAvailableResourceGroupsByName();

        Result<AzureResource> GetResourceGroup(string resourceGroupName);

        Result<AzureResource[]> GetAllResourcesInResourceGroup(string resourceGroupName);

        Result<AzureResource[]> GetResourceGroupExportTemplate(string resourceGroupName);
    }
}
