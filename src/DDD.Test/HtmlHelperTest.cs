using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Xunit;
using DDD.Common;

namespace DDD.Test
{
    public class HtmlHelperTest
    {
        [Fact]
        public void TestClearA()
        {
            string originStr = @"<div class='Login'>< !--登录前-- ><a target = '_blank' href = '/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml' > 登录 </a>< span >|</ span ><a target = '_blank' class='free_reg' href='/YslXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdxx.shtml'>注册</a></div>";
            string expectedStr = @"<div class='Login'>< !--登录前-- > 登录 < span >|</ span >注册</div>";
            string actualStr = HtmlHelper.RemoveA(originStr).ToString();
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void TestRemoveStyle()
        {
            string originStr = "<span style='line - height:2; font - size:16px;'>超远程赛鸽已经形成了</span>";
            originStr = System.Web.HttpUtility.HtmlDecode(originStr);
            string expectedStr = "<span>超远程赛鸽已经形成了</span>";
            string actualStr = HtmlHelper.RemoveStyle(originStr);
            Assert.Equal(expectedStr, actualStr);
        }


    }
}

