using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface ICmsContentsRespository
    {      
        void Add(CmsContents CmsContents);
        void Delete(CmsContents CmsContents);
        void UpdateClick(int id,int addClicks);
        CmsContents GetSingle(int CmsContentsId);
        IQueryable<CmsContents> GetList(int limit = 10,int start = 0,int orderType = 0);
        bool Save();
        Task<int> SaveAsync();
        bool IsExist(int CmsContentsId);
    }
}
