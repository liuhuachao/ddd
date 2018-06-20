using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.WebApi.Models;

namespace DDD.WebApi.Repositories
{
    public interface ICmsContentsRepository
    {
        IQueryable<CmsContents> GetList(int limit = 10, int start = 0, int orderType = 0);
        CmsContents GetSingle(int CmsId);
        bool IsExist(int CmsId);
        bool Save();
        Task<int> SaveAsync();
        IList<CmsContents> Search(string title);
    }
}