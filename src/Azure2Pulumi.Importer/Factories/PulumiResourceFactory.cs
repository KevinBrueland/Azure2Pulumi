using Azure2Pulumi.Importer.Common;
using Azure2Pulumi.Importer.DTOs;
using Azure2Pulumi.Importer.Interfaces.Factories;
using Azure2Pulumi.Importer.Models;
using System.Reflection;

namespace Azure2Pulumi.Importer.Factories
{
    public class PulumiResourceFactory : IPulumiResourceFactory
    {
        private Lazy<Dictionary<string, Type>> _typeMap;

        public PulumiResourceFactory()
        {

            _typeMap = new Lazy<Dictionary<string, Type>>(InitiateAzureToPulumiResourceTypeMap);
        }

        public Result<PulumiResource> Create(AzureResource resource, Context context)
        {
            if (_typeMap.Value.ContainsKey(resource.Type))
            {
                var type = _typeMap.Value[resource.Type];

                return ((ImportResource)Activator.CreateInstance(type)).Create(resource, context);
            }
            return new NotImplementedResource().Create(resource, context);
        }

        private Dictionary<string, Type> InitiateAzureToPulumiResourceTypeMap()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var pulumiResourceType = typeof(ImportResource);
            var derivedTypes = assembly.GetTypes()
                                       .Where(type => type.IsSubclassOf(pulumiResourceType) &&
                                                      !type.IsAbstract && type.IsClass)
                                       .ToList();

            var comparer = StringComparer.OrdinalIgnoreCase;
            var resourceDictionary = new Dictionary<string, Type>(comparer);

            foreach (var type in derivedTypes)
            {
                //For some reason, some resources in azure have the same type, so for now, we just grab the first and hope thats good enough.
                ImportResource instance = (ImportResource)Activator.CreateInstance(type);
                if (!resourceDictionary.ContainsKey(instance.AzureResourceType))
                    resourceDictionary.Add(instance.AzureResourceType, type);
            }

            return resourceDictionary;

        }
    }
}
