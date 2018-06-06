using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface ICmsContentsRespository
    {      
        void AddCmsContents(CmsContents CmsContents);
        void DeleteCmsContents(CmsContents CmsContents);
        void UpdateCmsContents(CmsContents CmsContents);
        CmsContents GetCmsContents(int CmsContentsId);
        Dtos.NewsList GetNews(int CmsContentsId);
        IQueryable<CmsContents> GetCmsContents(int limit = 10,int start = 0,int orderType = 0);
        IList<Dtos.NewsList> GetNewsList(int limit = 10, int start = 0, int orderType = 0);
        bool Save();
        Task<int> SaveAsync();
        bool IsExistCmsContents(int CmsContentsId);
    }
}
