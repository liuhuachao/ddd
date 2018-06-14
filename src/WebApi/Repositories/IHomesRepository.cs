using System.Collections.Generic;
using WebApi.Dtos;

namespace WebApi.Repositories
{
    public interface IHomesRepository
    {
        HomeDetail GetDetail(int id, int type);
        IList<HomeHotSearch> GetHotSearch(int limit = 10);
        IList<HomeList> GetList(int pageIndex = 1 , int pageSize = 8);
    }
}