using Microsoft.Extensions.Caching.Memory;

namespace SampleProject.Infrastructure.Caching;

public class MemoryCacheStore(
    IMemoryCache memoryCache,
    Dictionary<string, TimeSpan> expirationConfiguration) : ICacheStore
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly Dictionary<string, TimeSpan> _expirationConfiguration = expirationConfiguration;

    public void Add<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null)
    {
        var cachedObjectName = item!.GetType().Name;
        var timespan = expirationTime ?? _expirationConfiguration[cachedObjectName];

        _memoryCache.Set(key.CacheKey, item, timespan);
    }

    public void Add<TItem>(TItem item, ICacheKey<TItem> key, DateTime? absoluteExpiration = null)
    {
        DateTimeOffset offset;
        if (absoluteExpiration.HasValue)
        {
            offset = absoluteExpiration.Value;
        }
        else
        {
            offset = DateTimeOffset.MaxValue;
        }

        _memoryCache.Set(key.CacheKey, item, offset);
    }

    public TItem? Get<TItem>(ICacheKey<TItem> key) where TItem : class
    {
        if (_memoryCache.TryGetValue(key.CacheKey, out TItem? value))
        {
            return value;
        }

        return null;
    }

    public void Remove<TItem>(ICacheKey<TItem> key)
    {
        _memoryCache.Remove(key.CacheKey);
    }
}