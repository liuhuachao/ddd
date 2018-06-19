using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class MemoryCache : ICacheStorage
    {
        private IMemoryCache _memoryCache;

        public MemoryCache(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public void Remove(string key)
        {
            this._memoryCache.Remove(key);
        }

        public void Store(string key, object data)
        {
            this._memoryCache.Set(key,data);

            //设置相对过期时间2分钟
            this._memoryCache.Set(key, data, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(2)));

            //设置绝对过期时间2分钟
            this._memoryCache.Set(key, data, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));

            //缓存优先级 （程序压力大时，会根据优先级自动回收）
            this._memoryCache.Set(key, data, new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.NeverRemove));
        }

        public T Retrieve<T>(string key)
        {
            T itemStored = this._memoryCache.Get<T>(key);
            return itemStored;
        }

    }
}
