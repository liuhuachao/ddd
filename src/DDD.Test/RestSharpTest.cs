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
        public async Task TestHttpDelete()
        {
            string expectedStr = "";
            string actualStr = "";

            var client = new RestClient("http://192.168.1.128:88");
            var request = new RestRequest("/v1/homes/RemoveCache", Method.DELETE);
            request.AddParameter("id", 300);
            request.AddParameter("showType", 0);

            var response = await client.ExecuteTaskAsync<ResultMsg>(request);
            actualStr = response.Data.Code.ToString();

            Assert.Equal(expectedStr, actualStr);
        }

        
    }
}
