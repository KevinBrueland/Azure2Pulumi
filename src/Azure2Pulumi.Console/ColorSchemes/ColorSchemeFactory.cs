using System.Reflection;

namespace Azure2Pulumi.Console.ColorSchemes
{
    public class ColorSchemeFactory
    {
        private Lazy<Dictionary<string, Type>> _colorSchemeMap;

        public ColorSchemeFactory()
        {

            _colorSchemeMap = new Lazy<Dictionary<string, Type>>(InitiateColorSchemeMap);
        }

        public ColorScheme Create(string schemeName)
        {
            if (_colorSchemeMap.Value.ContainsKey(schemeName))
            {
                var colorScheme = _colorSchemeMap.Value[schemeName];

                return ((ColorScheme)Activator.CreateInstance(colorScheme));
            }

            return new DefaultColorScheme();
        }

        private Dictionary<string, Type> InitiateColorSchemeMap()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var pulumiResourceType = typeof(ColorScheme);
            var derivedTypes = assembly.GetTypes()
                                       .Where(type => type.IsSubclassOf(pulumiResourceType) &&
                                                      !type.IsAbstract && type.IsClass)
                                       .ToList();

            var comparer = StringComparer.OrdinalIgnoreCase;
            var resourceDictionary = new Dictionary<string, Type>(comparer);

            foreach (var type in derivedTypes)
            {
                //For some reason, some resources in azure have the same type, so for now, we just grab the first and hope thats good enough.
                ColorScheme instance = (ColorScheme)Activator.CreateInstance(type);
                if (!resourceDictionary.ContainsKey(instance.SchemeName))
                    resourceDictionary.Add(instance.SchemeName, type);
            }

            return resourceDictionary;

        }
    }
}
