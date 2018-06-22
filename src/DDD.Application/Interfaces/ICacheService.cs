using System;
using System.Collections.Generic;

namespace DDD.Application.Interfaces
{
    public interface ICacheService
    {
        bool Exists(string key);
        T Get<T>(string key) where T : class;
        bool Remove(string key);
        void RemoveAll(IEnumerable<string> keys);
        bool Set(string key, object data);
        bool Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
    }
}