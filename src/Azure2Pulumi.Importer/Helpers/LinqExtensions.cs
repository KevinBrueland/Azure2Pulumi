
namespace Azure2Pulumi.Importer.Helpers
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctByMultiple<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var known = new HashSet<TKey>();
            return source.Where(element => known.Add(keySelector(element)));
        }
    }
}
