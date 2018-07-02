using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DDD.Application.Interfaces;
using DDD.Application.Dtos;

namespace DDD.Application.Services
{
    /// <summary>
    /// 首页服务类
    /// 增加缓存
    /// </summary>
    public class HomeService : IHomeAppService
    {
        private readonly IHomesRepository _repository;
        private readonly ICacheService _cacheSevice;

        public HomeService(IHomesRepository repository, ICacheService cacheSevice)
        {
            this._repository = repository;
            this._cacheSevice = cacheSevice;
        }

        public IList<HomeList> GetList(int pageIndex = 1, int pageSize = 5)
        {
            IList<HomeList> homeList;
            string cacheKey = string.Format("home_list_{0}_{1}",pageIndex,pageSize);
            if (!this._cacheSevice.Exists(cacheKey))
            {
                homeList = this._repository.GetList(pageIndex, pageSize);
                if (homeList != null)
                {
                    this._cacheSevice.Set(cacheKey, homeList, TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(30));
                }
            }
            else
            {
                homeList = this._cacheSevice.Get<List<HomeList>>(cacheKey);
            }                      
            return homeList;
        }

        public HomeDetail GetDetail(int id, int type)
        {
            HomeDetail homeDetail;
            string cacheKey = string.Format("home_detail_{0}_{1}", id, type);
            homeDetail = this._cacheSevice.Get<HomeDetail>(cacheKey);
            if (homeDetail == null)
            {
                homeDetail = this._repository.GetDetail(id, type);
                if(homeDetail != null)
                {
                    this._cacheSevice.Set(cacheKey, homeDetail, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
                }                
            }
            return homeDetail;
        }

        public IList<HomeList> GetMore(int id, int type)
        {
            IList<HomeList> homeList;
            string cacheKey = string.Format("home_more_{0}_{1}", id, type);
            homeList = this._cacheSevice.Get<IList<HomeList>>(cacheKey);
            if (homeList == null)
            {
                homeList = this._repository.GetMore(id, type);
                if(homeList != null)
                {
                    this._cacheSevice.Set(cacheKey, homeList, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
                }                
            }
            return homeList;
        }

        public IList<HomeSearch> Search(string title)
        {
            IList<HomeSearch> homeSearch;
            string cacheKey = string.Format("home_search_{0}", title);
            homeSearch = this._cacheSevice.Get<List<HomeSearch>>(cacheKey);
            if (homeSearch == null)
            {
                homeSearch = this._repository.Search(title);
                if (homeSearch != null)
                {
                    this._cacheSevice.Set(cacheKey, homeSearch, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
                }                
            }
            return homeSearch;
        }

        public IList<HotSearch> HotSearch(int limit = 8)
        {
            IList<HotSearch> hotSearch;
            string cacheKey = string.Format("hot_search_{0}", limit);
            hotSearch = this._cacheSevice.Get<List<HotSearch>>(cacheKey);
            if (hotSearch == null)
            {
                hotSearch = this._repository.HotSearch(limit);
                if (hotSearch != null)
                {
                    this._cacheSevice.Set(cacheKey, hotSearch, TimeSpan.FromDays(1), TimeSpan.FromDays(1));
                }                
            }
            return hotSearch;
        }

        public bool ExistCache(string cacheKey)
        {
            return this._cacheSevice.Exists(cacheKey);
        }

        public bool RemoveCache(int id,int type)
        {
            string cacheKey2 = string.Format("home_list_{0}_{1}", 1, 5);
            string cacheKey1 = string.Format("home_detail_{0}_{1}", id, type);

            if (ExistCache(cacheKey1))
            {
                this._cacheSevice.Remove(cacheKey1);
            }
            if (ExistCache(cacheKey2))
            {
                this._cacheSevice.Remove(cacheKey2);
            }
            return !ExistCache(cacheKey1)&&!ExistCache(cacheKey2);
        }

    }
}
