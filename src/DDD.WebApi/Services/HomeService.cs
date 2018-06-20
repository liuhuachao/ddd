using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DDD.WebApi.Repositories;

namespace DDD.WebApi.Services
{
    /// <summary>
    /// 首页服务类
    /// 增加缓存
    /// </summary>
    public class HomeService : IHomeService
    {
        private readonly ILogger<HomeService> _logger;
        private readonly IHomesRepository _repository;
        private readonly ICacheService _cacheSevice;

        public HomeService(ILogger<HomeService> logger,IHomesRepository repository, ICacheService cacheSevice)
        {
            this._logger = logger;
            this._repository = repository;
            this._cacheSevice = cacheSevice;
        }

        public IList<Dtos.HomeList> GetList(int pageIndex = 1, int pageSize = 8)
        {
            IList<Dtos.HomeList> homeList;
            string storageKey = string.Format("home_list_{0}_{1}",pageIndex,pageSize);
            homeList = this._cacheSevice.Get<List<Dtos.HomeList>>(storageKey);
            if (homeList == null)
            {
                homeList = this._repository.GetList(pageIndex, pageSize);
                this._cacheSevice.Set(storageKey,homeList,TimeSpan.FromMinutes(1),TimeSpan.FromMinutes(1));
            }            
            return homeList;
        }

        public Dtos.HomeDetail GetDetail(int id, int type)
        {
            Dtos.HomeDetail homeDetail;
            string storageKey = string.Format("home_detail_{0}_{1}", id, type);
            homeDetail = this._cacheSevice.Get<Dtos.HomeDetail>(storageKey);
            if (homeDetail == null)
            {
                homeDetail = this._repository.GetDetail(id, type);
                this._cacheSevice.Set(storageKey, homeDetail,TimeSpan.FromHours(1), TimeSpan.FromHours(1));
            }
            return homeDetail;
        }

        public IList<Dtos.HomeList> GetMore(int id, int type)
        {
            IList<Dtos.HomeList> homeList;
            string storageKey = string.Format("home_more_{0}_{1}", id, type);
            homeList = this._cacheSevice.Get<IList<Dtos.HomeList>>(storageKey);
            if (homeList == null)
            {
                homeList = this._repository.GetMore(id, type);
                this._cacheSevice.Set(storageKey, homeList, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
            }
            return homeList;
        }

        public IList<Dtos.HomeSearch> Search(string title)
        {
            IList<Dtos.HomeSearch> homeSearch;
            string storageKey = string.Format("home_search_{0}", title);
            homeSearch = this._cacheSevice.Get<List<Dtos.HomeSearch>>(storageKey);
            if (homeSearch == null)
            {
                homeSearch = this._repository.Search(title);
                this._cacheSevice.Set(storageKey, homeSearch,TimeSpan.FromHours(1),TimeSpan.FromHours(1));
            }
            return homeSearch;
        }

        public IList<Dtos.HotSearch> HotSearch(int limit = 10)
        {
            IList<Dtos.HotSearch> hotSearch;
            string storageKey = string.Format("hot_search_{0}", limit);
            hotSearch = this._cacheSevice.Get<List<Dtos.HotSearch>>(storageKey);
            if (hotSearch == null)
            {
                hotSearch = this._repository.HotSearch(limit);
                this._cacheSevice.Set(storageKey, hotSearch,TimeSpan.FromDays(1), TimeSpan.FromDays(1));
            }
            return hotSearch;
        }

    }
}
