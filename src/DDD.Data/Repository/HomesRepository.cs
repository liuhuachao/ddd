using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DDD.Data;
using DDD.Domain.Entities;
using AutoMapper;
using DDD.Application.Dtos;
using DDD.Application.Interfaces;

namespace DDD.Data.Repositories
{
    public class HomesRepository : IHomesRepository
    {
        private readonly PigeonsContext _context;
        private readonly ILogger<HomesRepository> _logger;

        public HomesRepository(PigeonsContext pigeonsContext, ILogger<HomesRepository> logger)
        {
            _context = pigeonsContext;
            _logger = logger;
        }

        public IList<HomeList> GetList(int pageIndex = 1, int pageSize = 8)
        {
            var _pageSize = pageSize > 100 ? 100 : pageSize;
            IList<HomeList> homeList;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@pageIndex",pageIndex),
                    new SqlParameter("@pageSize",pageSize),
                    new SqlParameter("@totalCount",DbType.Int32),
                };
                parameters[2].Direction = ParameterDirection.Output;

                homeList = this._context.Set<HomeList>()
                    .FromSql("EXECUTE UP_App_GetHomeList @pageIndex,@pageSize,@totalCount OUTPUT", parameters).ToList();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this._logger.LogCritical(ex.Message);
                return null;
            }
            return homeList;
        }

        public HomeDetail GetDetail(int id, int type)
        {
            HomeDetail detail;
            try
            {
                 SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@showType",type),
                };
                  
                detail = this._context.Set<HomeDetail>()
                .FromSql("EXECUTE UP_App_GetDetail @id,@showType",parameters)
                .FirstOrDefault();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this._logger.LogCritical(ex.Message);
                return null;
            }
            return detail;
        }

        public IList<HomeList> GetMore(int id, int type)
        {
            IList<HomeList> homeMore;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
               {
                    new SqlParameter("@id",id),
                    new SqlParameter("@showType",type),
               };

                homeMore = this._context.Set<HomeList>()
                .FromSql("EXECUTE UP_App_GetMore @id,@showType", parameters)
                .ToList();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this._logger.LogCritical(ex.Message);
                return null;
            }
            return homeMore;
        }
 
        public IList<HomeSearch> Search(string title)
        {
            var limit = 10;

            IQueryable<HomeSearch> news = this._context.CmsContents
                .Where(c => EF.Functions.Like(c.CmsTitle, "%" + title + "%"))
                .OrderByDescending(x => x.CmsId)
                .Take(limit / 2)
                .Select(n => new HomeSearch()
                {
                    Id = n.CmsId,
                    Title = n.CmsTitle,
                    ShowType = 2,
                });

            IQueryable<HomeSearch> videos = this._context.VdVideo
                .Where(item => EF.Functions.Like(item.Title, "%" + title + "%"))
                .OrderByDescending(x => x.Id)
                .Take(limit / 2)
                .Select(v => new HomeSearch()
                {
                    Id = v.Id,
                    Title = v.Title,
                    ShowType = 3,
                });

            IList<HomeSearch> homeList = new List<HomeSearch>();

            foreach (var item in news)
            {
                homeList.Add(item);
            }
            foreach (var item in videos)
            {
                homeList.Add(item);
            }
            
            return homeList;
        }

        public IList<HotSearch> HotSearch(int limit = 10)
        {
            var _limit = limit > 100 ? 100 : limit;
            IList<HotSearch> homeList;
            try
            {
                homeList = this._context.Set<HotSearch>()
                .FromSql("EXECUTE UP_App_GetHotSearch").ToList();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this._logger.LogCritical(ex.Message);
                return null;
            }
            return homeList;
        }

        public async Task<int> UpdateLikes(int id,int showType)
        {
            var addLikes = new Random().Next(1, 10);
            dynamic model = null;
            if (showType == 3)
            {
                model = this._context.VdVideo.Find(id);               
            }
            else
            {
                model = this._context.CmsContents.Find(id);
            }
            model.Likes += addLikes;
            return await _context.SaveChangesAsync();
        }

        public bool IsExist(int id, int showType)
        {
            if (showType == 3)
            {
                return this._context.CmsContents.Any(x => x.CmsId == id);
            }
            else
            {
                return this._context.VdVideo.Any(x => x.Id == id);
            }
        }

    }
}
