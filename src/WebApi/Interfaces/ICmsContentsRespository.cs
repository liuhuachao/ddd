using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICmsContentsRespository
    {      
        CmsContents GetSingle(int CmsContentsId);
        IQueryable<CmsContents> GetList(int limit = 10,int start = 0,int orderType = 0);
        IList<CmsContents> Search(string title);
        bool Save();
        Task<int> SaveAsync();
        bool IsExist(int CmsContentsId);
    }
}
