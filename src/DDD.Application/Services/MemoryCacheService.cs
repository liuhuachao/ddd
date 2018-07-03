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

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return this._cache.TryGetValue(key,out object cacheObj);
        }

        /// <summary>
        /// 获取缓存（object）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public object Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.Get(key);
        }

        /// <summary>
        /// 获取缓存（泛型）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
             if (key == null)
             {
                  throw new ArgumentNullException(nameof(key));
             }
            return this._cache.Get(key) as T;
        }         

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="data">缓存Value</param>
        /// <returns></returns>
        public bool Set(string key, object data)
        {
            if (key == null || data == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            this._cache.Set(key,data);
            return Exists(key);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            this._cache.Remove(key);
            return !Exists(key);
        }

        /// 批量删除缓存
        /// </summary>
        /// <param name="key">缓存Key集合</param>
        /// <returns></returns>
        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            keys.ToList().ForEach(item => this._cache.Remove(item));
        }

        /// <summary>
        /// 释放缓存
        /// </summary>
        public void Dispose()
        {
            if (this._cache != null)
                this._cache.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
