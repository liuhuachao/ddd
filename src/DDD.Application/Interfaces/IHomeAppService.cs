using System.Collections.Generic;
using DDD.Application.Dtos;

namespace DDD.Application.Interfaces
{
    public interface IHomeAppService
    {
        HomeDetail GetDetail(int id, int type);
        IList<HomeList> GetMore(int id, int type);
        IList<HomeList> GetList(int pageIndex = 1, int pageSize = 8);
        IList<HotSearch> HotSearch(int limit = 10);
        IList<HomeSearch> Search(string title);
    }
}