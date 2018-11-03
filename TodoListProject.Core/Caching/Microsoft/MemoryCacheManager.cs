using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListProject.Core.Caching.Microsoft {
    public class MemoryCacheManager : ICacheManager {
        private IMemoryCache _memoryCache;
        public MemoryCacheManager(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key) {
            T response;
            var isExist = _memoryCache.TryGetValue(key, out response);
            return isExist ? response : default(T);
        }

        public void Add(string key, object data, int cacheTime) {
            if (data == null) {
                return;
            }

            var cacheExpirationOptions =
           new MemoryCacheEntryOptions {
               AbsoluteExpiration = DateTime.Now.AddMinutes(cacheTime),
               Priority = CacheItemPriority.Normal
           };
            _memoryCache.Set(key, data, cacheExpirationOptions);
        }

        public void Remove(string key) {
            _memoryCache.Remove(key);
        }
    }
}
