
namespace Azure2Pulumi.Importer.Helpers
{
    public static class PulumiResourceHelper
    {
        //Child names are usually nested in the template, with {parent1}/{parent2}../childName
        public static string GetNameIfNested(string name, string splitOn = "/")
        {
            return name.Contains(splitOn) ? name.Split(splitOn).Last() : name;
        }

        public static string[] GetParentNames(string[] parents)
        {
            if (parents != null && parents.Any())
            {
                var parentNames = parents[0].Replace(")]", "")
                                            .Replace("'", "")
                                            .Split(",")
                                            .ToList();
                return parentNames.Skip(1).Select(x => x.Trim()).ToArray();
            }

            return Array.Empty<string>();
        }
    }
}
