using System;
using System.Collections.Generic;
using System.Text;
using DDD.Application.Dtos;
using DDD.Application.Interfaces;
using DDD.Domain.Interfaces;
using AutoMapper;

namespace DDD.Application.Services
{
    public class VideosAppService : IVideosAppService
    {
        private readonly IVideosRepository _repositoty;
        private readonly ICacheService _cacheService;

        public VideosAppService(IVideosRepository repositoty, ICacheService cacheService)
        {
            this._repositoty = repositoty;
            this._cacheService = cacheService;
        }

        public VideoDetail GetDetail(int id)
        {
            VideoDetail detail;
            string cacheKey = string.Format("video_detail_{0}", id);
            detail = this._cacheService.Get<VideoDetail>(cacheKey);
            if (detail == null)
            {
                var result = this._repositoty.GetDetail(id);
                if (result != null)
                {
                    detail = Mapper.Map<VideoDetail>(result);
                    this._cacheService.Set(cacheKey, detail, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
                }
            }
            return detail;
        }
    }
}
