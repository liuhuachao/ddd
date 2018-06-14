using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Interfaces;

namespace WebApi.Repositories
{
    public class HomesRespository : IHomesRespository
    {
        private readonly PigeonsContext _context;
        private readonly ILogger<HomesRespository> _logger;

        public HomesRespository(PigeonsContext pigeonsContext, ILogger<HomesRespository> logger)
        {
            _context = pigeonsContext;
            _logger = logger;
        }

        public Dtos.HomeDetail GetDetail(int id, int type)
        {
            Dtos.HomeDetail detail;
            try
            {
                 SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@showType",type),
                };
                  
                detail = this._context.Set<Dtos.HomeDetail>()
                .FromSql("EXECUTE UP_App_GetDetail @id,@showType",parameters)
                .FirstOrDefault();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this._logger.LogCritical(ex.Message);
                return null;
                //throw;
            }
            return detail;
        }

        public IList<Dtos.HomeHotSearch> GetList(int limit = 10)
        {
            var _limit = limit > 100 ? 100 : limit;
            IList<Dtos.HomeHotSearch> homeList;
            try
            {
                homeList = this._context.Set<Dtos.HomeHotSearch>()
                .FromSql("EXECUTE UP_App_GetHotSearch").ToList();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this._logger.LogCritical(ex.Message);
                return null;               
            }
            return homeList;
        }
    }
}
