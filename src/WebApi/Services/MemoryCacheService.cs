using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Services
{
    public class MemoryCacheService : ICacheService
    {
        private IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
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
            object cached;
            return this._memoryCache.TryGetValue(key, out cached);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
             if (key == null)
             {
                  throw new ArgumentNullException(nameof(key));
             }
            return this._memoryCache.Get(key) as T;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="data">缓存Value</param>
        /// <returns></returns>
        public bool Set(string key, object data)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            this._memoryCache.Set(key,data);
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
        public bool Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
         {
             if (key == null)
             {
                 throw new ArgumentNullException(nameof(key));
             }
             if (value == null)
             {
                 throw new ArgumentNullException(nameof(value));
             }
            this._memoryCache.Set(key, value,
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
            this._memoryCache.Remove(key);
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

            keys.ToList().ForEach(item => this._memoryCache.Remove(item));
        }

        /// <summary>
        /// 释放缓存
        /// </summary>
        public void Dispose()
        {
            if (this._memoryCache != null)
                this._memoryCache.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
