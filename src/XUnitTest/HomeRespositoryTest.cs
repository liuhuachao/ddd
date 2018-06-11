using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using WebApi.Models;
using WebApi.Repositories;

namespace XUnitTest
{
    public class HomeRespositoryTest
    {
        private readonly IHomesRespository _respository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="respository">仓库</param>
        public HomeRespositoryTest(IHomesRespository respository)
        {
            _respository = respository;
        }

        [Fact]
        public void TestGetHotSearch()
        {
            var resultList = this._respository.GetList(10);
            IList<WebApi.Dtos.HomeList> homeList = new List<WebApi.Dtos.HomeList>();
            homeList.Add(
                new WebApi.Dtos.HomeList()
                {
                    Id = 1,
                    Title = ""
                }                
            );            
            Assert.Equal(homeList,resultList);
        }
    }
}
