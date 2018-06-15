using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Repositories
{
    public interface IHomesRepository
    {
        HomeDetail GetDetail(int id, int type);
        IList<HomeList> GetList(int pageIndex = 1, int pageSize = 8);
        IList<HotSearch> HotSearch(int limit = 10);
        bool IsExist(int id, int showType);
        IList<HomeSearch> Search(string title);
        Task<int> UpdateLikes(int id, int showType);
    }
}