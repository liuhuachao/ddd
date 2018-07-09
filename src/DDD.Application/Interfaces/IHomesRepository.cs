using System.Collections.Generic;
using System.Threading.Tasks;
using DDD.Application.Dtos;

namespace DDD.Application.Interfaces
{
    public interface IHomesRepository
    {        
        IList<HomeList> GetList(int pageIndex = 1, int pageSize = 8);
        HomeDetail GetDetail(int id, int type);
        IList<HomeList> GetMore(int id, int type);
        IList<HotSearch> HotSearch(int limit = 10);
        bool IsExist(int id, int showType);
        IList<HomeSearch> Search(string title);        
        Task<int> UpdateClicks(int id, int showType);
        Task<int> UpdateLikes(int id, int showType);
    }
}