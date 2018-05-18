using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class CmsContentsRepository : ICmsContentsRespository
    {
        private readonly PigeonsContext _context;

        public CmsContentsRepository(PigeonsContext pigeonsContext)
        {
            _context = pigeonsContext;
        }

        public void AddCmsContents(CmsContents CmsContents)
        {
            this._context.CmsContents.Add(CmsContents);
        }

        public void DeleteCmsContents(CmsContents CmsContents)
        {
            _context.CmsContents.Remove(CmsContents);
        }

        public void UpdateCmsContents(CmsContents cmsContents)
        {
            var content = this._context.CmsContents.Find(cmsContents.CmsId);            
            _context.SaveChanges();
        }

        public CmsContents GetCmsContents(int CmsId)
        {
            return _context.CmsContents.Find(CmsId);
        }

        public IList<CmsContents> GetCmsContents()
        {
            return _context.CmsContents.Skip(1).Take(10).OrderBy(x => x.CmsId).ToList();
        }

        public bool IsExistCmsContents(int CmsId)
        {
            return _context.CmsContents.Any(x => x.CmsId == CmsId);
        }

    }
}
