namespace Huy.Clothing.Caching.Common;

public static partial class CachingCommonDefaults
{
    public static int CacheTime = 15;//15 minutes

    public static string CacheKey => "huy.clothing.{0}.id.{1}";
    public static string AllCacheKey => "huy.clothing.{0}.all";
    public static string CacheKeyHeader => "huy.clothing.{0}.id";
}
