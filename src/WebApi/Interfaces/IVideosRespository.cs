using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IVideosRespository
    {
        VdVideo GetSingle(int Id);
        IQueryable<VdVideo> GetList(int limit = 10, int start = 0, int orderType = 0);
        IList<VdVideo> Search(string title);
        bool Save();
        Task<int> SaveAsync();
        bool IsExist(int Id);
    }
}
