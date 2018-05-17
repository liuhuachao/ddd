using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface ICmsContentsRespository
    {      
        void AddCmsContents(CmsContents CmsContents);
        void DeleteCmsContents(CmsContents CmsContents);
        void UpdateCmsContents(CmsContents CmsContents);
        CmsContents GetCmsContents(int CmsContentsId);
        IList<CmsContents> GetCmsContents();

        bool IsExistCmsContents(int CmsContentsId);

    }
}
