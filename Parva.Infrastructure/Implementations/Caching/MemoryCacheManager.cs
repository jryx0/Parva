using Parva.Application.Interfaces.Caching;
using System.Runtime.Caching;

namespace Parva.Infrastructure.Implementations.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private ObjectCache _cache = MemoryCache.Default;

        public T Get<T>(string key)
        {
            return (T)_cache[key];
        }

        public void Set(string key, object data)
        {
            if (data == null)
            {
                return;
            }

            var policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.NotRemovable;
            _cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsSet(string key)
        {
            return (_cache.Contains(key));
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            foreach (var item in _cache)
            {
                Remove(item.Key);
            }
        }
    }
}