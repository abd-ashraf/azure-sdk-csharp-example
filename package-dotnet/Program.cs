using Azure;
using Azure.Identity;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;

class Program
{
    static async Task Main(string[] args)
    {
        string subscriptionId = "b9f735a7-1300-40a3-807b-4445764b3e96";
        string resourceGroupName = "my-sdk-rg";
        string storageAccountName = "mystorage123abc";
        string region = "westus";  // e.g., "westus"

        // First, initialize the ArmClient and get the default subscription
        ArmClient client = new ArmClient(new DefaultAzureCredential());

        // Now we get a ResourceGroupResource collection for that subscription
        SubscriptionResource subscription = await client.GetDefaultSubscriptionAsync();
        ResourceGroupCollection resourceGroups = subscription.GetResourceGroups();

        // With the collection, we can create a new resource group with an specific name
        AzureLocation location = AzureLocation.WestUS2;
        ResourceGroupData resourceGroupData = new ResourceGroupData(location);
        ArmOperation<ResourceGroupResource> operation = await resourceGroups.CreateOrUpdateAsync(WaitUntil.Completed, resourceGroupName, resourceGroupData);
        ResourceGroupResource resourceGroup = operation.Value;

        Console.WriteLine($"Resource Group created Successfully.");
    }
}
