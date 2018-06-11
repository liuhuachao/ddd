using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class HomesRespository : IHomesRespository
    {
        private readonly PigeonsContext _context;

        public HomesRespository(PigeonsContext pigeonsContext)
        {
            _context = pigeonsContext;
        }

        public IList<Dtos.HomeHotSearch> GetList(int limit = 10)
        {
            var _limit = limit > 100 ? 100 : limit;
            IList<Dtos.HomeHotSearch> homeList = this._context.Set<Dtos.HomeHotSearch>()
                .FromSql("EXECUTE UP_GetHotSearch").ToList();
            return homeList;
        }
    }
}
