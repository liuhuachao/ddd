using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface ICacheStorage
    {
        void Remove(string key);
        void Store(string key,object data);
        T Retrieve<T>(string key);
    }
}
