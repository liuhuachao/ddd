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
        private readonly ICmsContentsRepository _repositoty;
        private readonly ICacheService _cacheService;

        public NewsAppService(ICmsContentsRepository repositoty, ICacheService cacheService)
        {
            this._repositoty = repositoty;
            this._cacheService = cacheService;
        }

        public NewsDetail GetDetail(int id)
        {
            NewsDetail detail;
            string cacheKey = string.Format("news_detail_{0}",id);
            detail = this._cacheService.Get<NewsDetail>(cacheKey);
            if (detail == null)
            {
                var result = this._repositoty.GetDetail(id);
                if(result != null)
                {
                    detail = Mapper.Map<NewsDetail>(result);
                    this._cacheService.Set(cacheKey, detail, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
                }
            }
            return detail;
        }       

    }
}
