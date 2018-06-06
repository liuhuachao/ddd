using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;
using AutoMapper;

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

        public Dtos.NewsList GetNews(int CmsId)
        {
            var contents = GetCmsContents(CmsId);
            var results = new Dtos.NewsList()
            {
                Id = contents.CmsId,
                Title = contents.CmsTitle,
                Intro = contents.CmsKeys,
                CoverImg = contents.CmsPhotos,
                Author = contents.CmsAuthor,
                PostTime = contents.OprateDate.ToString(),
            };
            return results;
        }

        public IQueryable<CmsContents> GetCmsContents(int limit = 10,int start = 0,int orderType = 0)
        {
            var _limit = limit > 100 ? 100 : limit;
            IQueryable<CmsContents> contents;
            if (orderType == 0)
            {
                contents = this._context.CmsContents.OrderByDescending(x => x.CmsId).Skip(start).Take(_limit);
            }
            else
            {
                contents = this._context.CmsContents.OrderBy(x => x.CmsId).Skip(start).Take(_limit);
            }
            return contents;
        }

        public IList<Dtos.NewsList> GetNewsList(int limit = 10, int start = 0, int orderType = 0)
        {
            var contents = GetCmsContents(limit, start, orderType);
            var results = Mapper.Map<IEnumerable<Dtos.NewsList>>(contents);
            return results.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool IsExistCmsContents(int CmsId)
        {
            return _context.CmsContents.Any(x => x.CmsId == CmsId);
        }

    }
}
