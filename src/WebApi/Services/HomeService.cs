using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi.Repositories;
using WebApi.Interfaces;

namespace WebApi.Services
{
    /// <summary>
    /// 首页服务类
    /// 增加缓存
    /// </summary>
    public class HomeService
    {
        private readonly ILogger<HomeService> _logger;
        private readonly IHomesRepository _repository;
        private readonly ICacheStorage _cacheStorage;

        public HomeService(ILogger<HomeService> logger,IHomesRepository repository,ICacheStorage cacheStorage)
        {
            this._logger = logger;
            this._repository = repository;
            this._cacheStorage = cacheStorage;
        }

        public IList<Dtos.HomeList> GetList(int pageIndex = 1, int pageSize = 8)
        {
            IList<Dtos.HomeList> homeList;
            string storageKey = string.Format("home_list_{0}_{1}",pageIndex,pageSize);
            homeList = this._cacheStorage.Retrieve<List<Dtos.HomeList>>(storageKey);
            if (homeList == null)
            {
                homeList = this._repository.GetList(pageIndex, pageSize);
                this._cacheStorage.Store(storageKey,homeList);
            }            
            return homeList;
        }

        public Dtos.HomeDetail GetDetail(int id, int type)
        {
            Dtos.HomeDetail homeDetail;
            string storageKey = string.Format("home_detail_{0}_{1}", id, type);
            homeDetail = this._cacheStorage.Retrieve<Dtos.HomeDetail>(storageKey);
            if (homeDetail == null)
            {
                homeDetail = this._repository.GetDetail(id, type);
                this._cacheStorage.Store(storageKey, homeDetail);
            }
            return homeDetail;
        }

        public IList<Dtos.HomeSearch> Search(string title)
        {
            IList<Dtos.HomeSearch> homeSearch;
            string storageKey = string.Format("home_search_{0}", title);
            homeSearch = this._cacheStorage.Retrieve<List<Dtos.HomeSearch>>(storageKey);
            if (homeSearch == null)
            {
                homeSearch = this._repository.Search(title);
                this._cacheStorage.Store(storageKey, homeSearch);
            }
            return homeSearch;
        }

        public IList<Dtos.HotSearch> HotSearch(int limit = 10)
        {
            IList<Dtos.HotSearch> hotSearch;
            string storageKey = string.Format("hot_search_{0}", limit);
            hotSearch = this._cacheStorage.Retrieve<List<Dtos.HotSearch>>(storageKey);
            if (hotSearch == null)
            {
                hotSearch = this._repository.HotSearch(limit);
                this._cacheStorage.Store(storageKey, hotSearch);
            }
            return hotSearch;
        }

    }
}
