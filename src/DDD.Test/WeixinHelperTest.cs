using System;
using Xunit;
using DDD.Common;
using System.Collections.Generic;

namespace DDD.Test
{
    public class WeixinHelperTest
    {
        [Fact]
        public void TestCreateJsSign()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();            
            dic.Add("timestamp", "123456789");
            dic.Add("url", "http://m.chsgw.com/news/detail/200");
            dic.Add("noncestr", "abcd");            
            string expectedStr = WeixinHelper.Sha1("noncestr=abcd&timestamp=123456789&url=http://m.chsgw.com/news/detail/200").ToLower();
            string actualStr = WeixinHelper.CreateSignForJsSdk(dic);
            Assert.Equal(expectedStr, actualStr);
        }
    }
}
