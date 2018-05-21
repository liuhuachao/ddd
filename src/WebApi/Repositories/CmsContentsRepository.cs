using AutoMapper;
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

        public IQueryable<CmsContents> GetCmsContents(int limit = 10,int start = 0,int orderType = 0)
        {
            IQueryable<CmsContents> contents;
            if (orderType == 0)
            {
                contents = this._context.CmsContents.OrderByDescending(x => x.CmsId).Skip(start).Take(limit);
            }
            else
            {
                contents = this._context.CmsContents.OrderBy(x => x.CmsId).Skip(start).Take(limit);
            }
            return contents;
        }

        public IList<Dtos.NewsRead> GetNewsList(int limit = 10, int start = 0, int orderType = 0)
        {
            var contents = GetCmsContents(limit, start, orderType);
            var results = Mapper.Map<IEnumerable<Dtos.NewsRead>>(contents);
            return results.ToList();
        }

        public CmsContents GetCmsContents(int CmsId)
        {
            return _context.CmsContents.Find(CmsId);
        }

        public bool IsExistCmsContents(int CmsId)
        {
            return _context.CmsContents.Any(x => x.CmsId == CmsId);
        }

    }
}
