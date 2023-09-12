using Huy.Clothing.Caching.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Huy.Clothing.Caching;

public class DataCached:IDataCached
{
    private readonly IMemoryCache _memoryCache;
    private static readonly ConcurrentDictionary<string, bool> _allKey;
    public CancellationTokenSource _cancellationTokenSource;
    static DataCached()
    {
        _allKey = new ConcurrentDictionary<string, bool>();
    }

    public DataCached(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public T Get<T>(string key, Func<T> accquire, int? cacheTime = null)
    {
        //item already is in cached, so return it
        if(_memoryCache.TryGetValue(key, out T? value))
        {
            return value!;
        }
        //or create it using passed function
        var result = accquire();
        //and set in cached (if cached time id defined)
        if((cacheTime?? CachingCommonDefaults.CacheTime) > 0)
        {
            Set(key, result,cacheTime?? CachingCommonDefaults.CacheTime);
        }
        return result;
    }

    public IList<string> GetKeys() => _allKey.Keys.ToList();

    public IList<T> GetValues<T>(string pattern)
    {
        //Collect all data from the memcached
        IList<T> values = new List<T>();
        //get cache keys that matches pattern
        var regex = new Regex(pattern,RegexOptions.Singleline|RegexOptions.Compiled|
            RegexOptions.IgnoreCase);
        var matchesKeys = _allKey
                          .Where(p => p.Value)
                          .Select(p => p.Key)
                          .Where(key => regex.IsMatch(key)).ToList();

        //loop to get value
        foreach( var key in matchesKeys)
        {
            _memoryCache.TryGetValue(key, out T? value);
            if(value != null)
            {
                values.Add(value);
            }
        }
        return values;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(_removeKey(key));//remove data and key in dictionary
    }

    public void Set<T>(string key, T? value, int cacheTime)
    {
        if(value != null)
        {
            _memoryCache.Set(_addKey(key),value,
                _getMemoryCacheEntryOptions(TimeSpan.FromMinutes(cacheTime)));
        }
    }
    private MemoryCacheEntryOptions _getMemoryCacheEntryOptions(TimeSpan? cacheTime)
    {
        var options = new MemoryCacheEntryOptions()
            .AddExpirationToken(new CancellationChangeToken(_cancellationTokenSource.Token))
            .SetSize(0);
        options.AbsoluteExpirationRelativeToNow = cacheTime;
        return options;

    }
    private string _addKey(string key)
    {
        _allKey.TryAdd(key, true);
        return key;
    }
    private string _removeKey(string key)
    {
        _tryRemoveKey(key);
        return key;
    }
    private void _tryRemoveKey(string key)
    {
        //try to remove key from dictionary
        if(!_allKey.TryRemove(key,out _))
        {
            _allKey.TryUpdate(key, false, true);
        }
    }

    public T Get<T>(string key)
    {
        if (_memoryCache.TryGetValue(key, out T? value))
            return value!;
        return default(T)!;
    }
    public object Get(string key)
    {
        if (_memoryCache.TryGetValue(key, out object? value))
            return value!;
        return null!;
    }

    public bool IsSet(string key) => _memoryCache.TryGetValue(key, out object _); 
}
