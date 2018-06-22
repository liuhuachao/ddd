using System;
using System.Collections.Generic;
using System.Text;
using DDD.Application.Dtos;
using DDD.Application.Interfaces;
using DDD.Domain.Interfaces;
using AutoMapper;

namespace DDD.Application.Services
{
    public class NewsAppService : INewsAppService
    {
        private readonly ICmsContentsRepository _contensRepositoty;
        private readonly ICacheService _cacheService;

        public NewsAppService(ICmsContentsRepository contentsRepositoty, ICacheService cacheService)
        {
            this._contensRepositoty = contentsRepositoty;
            this._cacheService = cacheService;
        }

        public NewsDetail GetDetail(int id)
        {
            NewsDetail newsDetail;
            string cacheKey = string.Format("news_detail_{0}",id);
            newsDetail = this._cacheService.Get<NewsDetail>(cacheKey);
            if (newsDetail == null)
            {
                var contents = this._contensRepositoty.GetDetail(id);
                if(contents != null)
                {
                    newsDetail = Mapper.Map<NewsDetail>(contents);
                    this._cacheService.Set(cacheKey, newsDetail, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
                }
            }
            return newsDetail;

        }       

    }
}
