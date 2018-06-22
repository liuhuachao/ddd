using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Domain.Entities;

namespace DDD.Domain.Interfaces
{
    public interface IVideosRepository
    {
        IQueryable<VdVideo> GetList(int limit = 10, int start = 0, int orderType = 0);
        VdVideo GetDetail(int Id);
        bool IsExist(int id);
        bool Save();
        Task<int> SaveAsync();
        IList<VdVideo> Search(string title);
    }
}