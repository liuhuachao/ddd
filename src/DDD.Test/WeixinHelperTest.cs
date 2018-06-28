using System;
using Xunit;
using DDD.Common;

namespace DDD.Test
{
    public class WeixinHelperTest
    {
        [Fact]
        public void TestCreateSign()
        {
            string[] args = new string[] { "noncestr", "jsapi_ticket", "timestamp","url" };
            //string originStr = @"";
            string expectedStr = "";
            string actualStr = WeixinHelper.CreateSign(args);
            Assert.Equal(expectedStr, actualStr);
        }
    }
}
