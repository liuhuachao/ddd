using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class NullObjectCache : ICacheStorage
    {
        public void Remove(string key)
        {
            // Do nothing
        }

        public void Store(string key, object data)
        {
            // Do nothing
        }

        public T Retrieve<T>(string key)
        {
            return default(T);
        }
    }
}
