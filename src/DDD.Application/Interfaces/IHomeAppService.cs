﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DDD.Application.Dtos;

namespace DDD.Application.Interfaces
{
    public interface IHomeAppService
    {
        HomeDetail GetDetail(int id, int type);
        IList<HomeList> GetMore(int id, int type);
        IList<HomeList> GetList(int pageIndex = 1, int pageSize = 10);
        IList<HomeList> HotSearch(int limit = 8);
        IList<HomeList> Search(string title);
        bool RemoveCache(int id,int type);
        Task<int> UpdateClicks(int id, int type);
        Task<int> UpdateLikes(int id, int type);
    }
}