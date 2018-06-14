using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IHomesRespository
    {
        Dtos.HomeDetail GetDetail(int id,int type);      
        IList<Dtos.HomeHotSearch> GetList(int limit = 10);
    }
}
