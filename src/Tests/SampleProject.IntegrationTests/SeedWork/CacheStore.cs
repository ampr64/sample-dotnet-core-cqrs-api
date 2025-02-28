using SampleProject.Infrastructure.Caching;
using System.Collections.Specialized;

namespace SampleProject.IntegrationTests.SeedWork;

public class CacheStore : ICacheStore
{
    private readonly ListDictionary _dictionary = [];
    public void Add<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null)
    {
        _dictionary.Add(key, item);
    }

    public void Add<TItem>(TItem item, ICacheKey<TItem> key, DateTime? absoluteExpiration = null)
    {
        _dictionary.Add(key, item);
    }

    public TItem? Get<TItem>(ICacheKey<TItem> key) where TItem : class
    {
        return _dictionary[key] as TItem;
    }

    public void Remove<TItem>(ICacheKey<TItem> key)
    {
        _dictionary.Remove(key);
    }
}