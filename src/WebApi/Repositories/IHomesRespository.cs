using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IHomesRespository
    {
        IList<Dtos.HomeHotSearch> GetList(int limit = 10);
    }
}
