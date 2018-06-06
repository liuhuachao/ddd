using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class VideosRespository:IVideosRespository
    {
        private readonly PigeonsContext _context;

        public VideosRespository(PigeonsContext pigeonsContext)
        {
            _context = pigeonsContext;
        }

        public VdVideo GetVideo(int Id)
        {
            return _context.VdVideo.Find(Id);
        }

        public IQueryable<VdVideo> GetVideos(int limit = 10, int start = 0, int orderType = 0)
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
