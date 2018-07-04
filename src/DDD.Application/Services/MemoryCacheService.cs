using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Application.Interfaces;

namespace DDD.Application.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public bool Exists(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return this._cache.TryGetValue(key,out object cacheObj);
        }

        public object Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.Get(key);
        }

        public T Get<T>(string key) where T : class
        {
             if (key == null)
             {
                  throw new ArgumentNullException(nameof(key));
             }
            return this._cache.Get(key) as T;
        }         

        public bool Set(string key, object data)
        {
            if (key == null || data == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            this._cache.Set(key,data);
            return Exists(key);
        }

        public bool Set(string key, object data, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null || data == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            this._cache.Set(key, data,
                    new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(expiresSliding)
                    .SetAbsoluteExpiration(expiressAbsoulte)                    
                    ); 
            return Exists(key);
        }

        public bool Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            this._cache.Remove(key);
            return !Exists(key);
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            keys.ToList().ForEach(item => this._cache.Remove(item));
        }

        public bool Replace(string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (Exists(key))
            {
                if (!Remove(key))
                    return false;
            }
            return Set(key, value);
        }

        public bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (Exists(key))
            {
                if (!Remove(key))
                    return false;
            }
            return Set(key, value, expiresSliding, expiressAbsoulte);
        }

        public void Dispose()
        {
            if (this._cache != null)
                this._cache.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
