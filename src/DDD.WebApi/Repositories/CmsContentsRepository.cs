using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DDD.WebApi.Models;

namespace DDD.WebApi.Repositories
{
    public class CmsContentsRepository : ICmsContentsRepository
    {
        private readonly PigeonsContext _context;

        public CmsContentsRepository(PigeonsContext pigeonsContext)
        {
            _context = pigeonsContext;
        }

        public CmsContents GetSingle(int CmsId)
        {
            return _context.CmsContents.Find(CmsId);
        }

        public IQueryable<CmsContents> GetList(int limit = 10,int start = 0,int orderType = 0)
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

        public IList<CmsContents> Search(string title)
        {
            var limit = 10;
            var start = 0;
            IList<CmsContents> contents = this._context.CmsContents
                .Where(content => EF.Functions.Like(content.CmsTitle,"%"+ title + "%"))
                .OrderByDescending(x => x.CmsId).Skip(start).Take(limit)
                .ToList();            
            return contents;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool IsExist(int CmsId)
        {
            return _context.CmsContents.Any(x => x.CmsId == CmsId);
        }
    }
}