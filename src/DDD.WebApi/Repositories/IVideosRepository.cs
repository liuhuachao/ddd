using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Data;
using DDD.Domain.Entities;

namespace DDD.WebApi.Repositories
{
    public interface IVideosRepository
    {
        IQueryable<VdVideo> GetList(int limit = 10, int start = 0, int orderType = 0);
        VdVideo GetSingle(int Id);
        bool IsExist(int id);
        bool Save();
        Task<int> SaveAsync();
        IList<VdVideo> Search(string title);
    }
}