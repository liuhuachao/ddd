using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Domain.Entities;

namespace DDD.Domain.Interfaces
{
    public interface ICmsContentsRepository
    {
        IQueryable<CmsContents> GetList(int limit = 10, int start = 0, int orderType = 0);
        CmsContents GetDetail(int CmsId);
        bool IsExist(int CmsId);
        bool Save();
        Task<int> SaveAsync();
        IList<CmsContents> Search(string title);
    }
}