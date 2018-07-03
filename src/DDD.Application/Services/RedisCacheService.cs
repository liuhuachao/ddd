using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDD.Application.Interfaces;
using DDD.Common;
using Microsoft.Extensions.Caching.Redis;
using StackExchange.Redis;

namespace DDD.Application.Services
{
    public class RedisCacheService : ICacheService
    {
        protected IDatabase _cache;
        private ConnectionMultiplexer _connection;
        private readonly string _instance;

        public RedisCacheService(RedisCacheOptions options, int database = 0)
        {
            _connection = ConnectionMultiplexer.Connect(options.Configuration);
            _cache = _connection.GetDatabase(database);
            _instance = options.InstanceName;
        }

        private string GetKeyForRedis(string key)
        {
            return _instance + "_" + key;
        }

        public bool Exists(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyExists(GetKeyForRedis(key));
        }

        public object Get(string key)
        {
             if (key == null)
             {
                 throw new ArgumentNullException(nameof(key));
             } 
             var value = _cache.StringGet(GetKeyForRedis(key)); 
             if(!value.HasValue)
             {
                 return null;
             }
            return JsonHelper.JsonToObject<object>(value);
        }

        public T Get<T>(string key) where T : class
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var value = _cache.StringGet(GetKeyForRedis(key));            
            if (!value.HasValue)
            {
                return default(T);
            }
             return JsonHelper.JsonToObject<T>(value);
        }
        
        public bool Set(string key, object data)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            key = GetKeyForRedis(key);
            var value = Encoding.UTF8.GetBytes(JsonHelper.ObjectToJson(data));
            return _cache.StringSet(key, value);
        }

        public bool Set(string key, object data, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
               throw new ArgumentNullException(nameof(key));
            }
            key = GetKeyForRedis(key);
            var value = Encoding.UTF8.GetBytes(JsonHelper.ObjectToJson(data));
            return _cache.StringSet(key, value, expiressAbsoulte);
        }

        public bool Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            key = GetKeyForRedis(key);
            return _cache.KeyDelete(key);
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            keys.ToList().ForEach(item => Remove(GetKeyForRedis(item)));
        }

    }
}
