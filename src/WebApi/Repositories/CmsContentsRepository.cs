using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class CmsContentsRepository : ICmsContentsRespository
    {
        private readonly PigeonsContext _pigeonsContext;

        public CmsContentsRepository(PigeonsContext context)
        {
            _pigeonsContext = context;
        }

        public void AddCmsContents(CmsContents CmsContents)
        {
            this._pigeonsContext.CmsContents.Add(CmsContents);
        }

        public void DeleteCmsContents(CmsContents CmsContents)
        {
            _pigeonsContext.CmsContents.Remove(CmsContents);
        }

        public void UpdateCmsContents(CmsContents CmsContents)
        {

        }

        public CmsContents GetCmsContents(int CmsId)
        {
            return _pigeonsContext.CmsContents.Where(x => x.CmsId == CmsId).FirstOrDefault();
        }

        public IList<CmsContents> GetCmsContents()
        {
            return _pigeonsContext.CmsContents.OrderBy(x => x.CmsId).ToList();
        }

        public bool IsExistCmsContents(int CmsId)
        {
            return _pigeonsContext.CmsContents.Any(x => x.CmsId == CmsId);
        }

    }
}
