using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebApiTest
{
    public class HtmlHelperTest
    {
        [Fact]
        public void TestEncodeAndDecode()
        {
            string originStr = @"&lt;p&gt;一．指定鸽规则&lt;/p&gt;\r<br>\r<br>&lt;p&gt;1.本次指定鸽分为：&nbsp;200元组、500元组、1000元组、&amp;nbsp;&amp;nbsp;&amp;nbsp;&nbsp;2000元组、&nbsp;5000元组。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;&amp;nbsp;(1).200元组、500元组、1000元组、各组均取前5名。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;奖金分配为：冠军30％、亚军25％、季军15％、四名10％、五名10％。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&nbsp;(2).2000元组取三名，奖金分配为：一名40％、二名30％、三名20％。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;（3）5000元组取两名。奖金分配为：一名50％、二名40％。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;二．300公里精英赛规程&lt;/p&gt;\r<br>\r<br>&lt;p&gt;每羽参赛费1000元，每11羽取1名，奖金10000元，不足羽数按90%发放。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;三．300公里预赛800元幸运一把抓幸运号777名以后，含777名次靠前者为胜取一名奖金90%发放。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;四．300公里单关别克GL8。2014款&nbsp;2.4L&nbsp;经典版&lt;/p&gt;\r<br>\r<br>&lt;p&gt;每羽参赛费2000元，110羽为一组，取1名。不足110羽以参赛费的90％发放，以此类推。200公里后确定环号汇款，评判标准以300公里名次靠前者为胜。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;五.注意事项：&lt;/p&gt;\r<br>\r<br>&lt;p&gt;&amp;nbsp;&nbsp;（1）本次指定鸽，精英赛必须由鸽友本人指定&lt;/p&gt;\r<br>\r<br>&lt;p&gt;&amp;nbsp;&nbsp;（2）汇款指定收费截止到上笼当天晚20：00结束&lt;/p&gt;\r<br>\r<br>&lt;p&gt;&amp;nbsp;&nbsp;（3）汇款指定请将汇款单与指定环号和指定明细以传真形&lt;/p&gt;\r<br>\r<br>&lt;p&gt;式发送至我公棚，口头指定与电话指定不予受理。&lt;/p&gt;\r<br>\r<br>&lt;p&gt;&amp;nbsp;（4）参赛鸽的各种资料需填写清晰并认真核对，传至我公棚，如出现填写错误与我公棚无关并取消指定资格，不退予指定费。&lt;/p&gt;";
            string encodeStr = originStr;
            string decodeStr1 = WebApi.Common.HtmlHelper.Decode(encodeStr);
            string decodeStr2 = System.Web.HttpUtility.HtmlDecode(encodeStr);
            Assert.Equal(decodeStr1, decodeStr1);
        }
    }
}

