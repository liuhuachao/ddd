using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Xunit;
using DDD.Application.Dtos;
using System.Threading.Tasks;

namespace DDD.Test
{
    public class RestSharpTest
    {
        [Fact]
        public async Task TestHttpGet()
        {
            string expectedStr = "200";
            string actualStr = "";

            var client = new RestClient("http://api.chsgw.com/");
            var request = new RestRequest("v1/Homes/GetDetail", Method.GET);

            request.AddParameter("id", 300);
            request.AddParameter("showType", 3);

            IRestResponse<ResultMsg> response = await client.ExecuteTaskAsync<ResultMsg>(request);
            actualStr = response.Data.Code.ToString();

            Assert.Equal(expectedStr, actualStr);
        }

        
    }
}
