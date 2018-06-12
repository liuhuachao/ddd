using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Models;

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
                //throw;
            }

            return homeList;
        }
    }
}
