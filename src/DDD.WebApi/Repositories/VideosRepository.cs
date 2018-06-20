using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.WebApi.Models;

namespace DDD.WebApi.Repositories
{
    public class VideosRepository : IVideosRepository
    {
        private readonly PigeonsContext _context;

        public VideosRepository(PigeonsContext pigeonsContext)
        {
            _context = pigeonsContext;
        }

        public VdVideo GetSingle(int Id)
        {
            return _context.VdVideo.Find(Id);
        }

        public IQueryable<VdVideo> GetList(int limit = 10, int start = 0, int orderType = 0)
        {
            var _limit = limit > 100 ? 100 : limit;
            IQueryable<VdVideo> videos;
            if (orderType == 0)
            {
                videos = this._context.VdVideo.OrderByDescending(x => x.Id).Skip(start).Take(_limit);
            }
            else
            {
                videos = this._context.VdVideo.OrderBy(x => x.Id).Skip(start).Take(_limit);
            }
            return videos;
        }

        public IList<VdVideo> Search(string title)
        {
            var limit = 10;
            var start = 0;
            IList<VdVideo> videoList = this._context.VdVideo
                .Where(item => EF.Functions.Like(item.Title, "%" + title + "%"))
                .OrderByDescending(x => x.Id).Skip(start).Take(limit)
                .ToList();
            return videoList;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool IsExist(int id)
        {
            return _context.VdVideo.Any(x => x.Id == id);
        }
    }
}
