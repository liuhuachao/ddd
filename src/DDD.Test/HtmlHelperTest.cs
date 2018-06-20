using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDD.Test
{
    public class HtmlHelperTest
    {
        [Fact]
        public void TestEncodeAndDecode()
        {
            string originStr = @"&lt;p&gt; <br> &lt;span&nbsp;style=&quot;font-family:SimSun;font-size:16px;&quot;&gt;&amp;nbsp;&nbsp;&amp;nbsp;&nbsp;鸽子游棚，是每个鸽友都会遇到的事情，一旦发生这种情况，鸽主难免会有心痛或惋惜的感觉，当然，偶尔也会有失而复得的惊喜。&lt;/span&gt; <br>&lt;/p&gt; <br>&lt;p&gt; <br> &lt;span&nbsp;style=&quot;font-family:SimSun;font-size:16px;&quot;&gt;&amp;nbsp;&nbsp;&amp;nbsp;&nbsp;但是，前不久在网上看到了这样一个视频，这位鸽友的经历与众不同，简直令人啼笑皆非，他的一羽游棚鸽让他遭受了一次善良的嘲讽、辛辣的幽默。&lt;/span&gt; <br>&lt;/p&gt; <br>&lt;p&gt; <br> &lt;span&nbsp;style=&quot;font-family:SimSun;font-size:16px;&quot;&gt;&amp;nbsp;&nbsp;&amp;nbsp;&nbsp;事情的原委是这样的：这位鸽友有一只鸽子，在家飞时没能随自家鸽群一起回来，最后回来的时候，腿上竟然多了一串东西。&lt;/span&gt; <br>&lt;/p&gt; <br>&lt;p&gt; <br> &lt;div&nbsp;style=&quot;text-align:center;&quot;&gt; <br>  &lt;a&nbsp;href=&quot;http://image.chsgw.com/2018061314371937190038.jpg&quot;&nbsp;title=&quot;36d89990e869827ef7408c225be0756a.jpg&quot;&nbsp;target=&quot;_blank&quot;&gt;&lt;img&nbsp;src=&quot;http://image.chsgw.com/2018061314371937190038.jpg&quot;&nbsp;title=&quot;36d89990e869827ef7408c225be0756a.jpg&quot;&nbsp;alt=&quot;36d89990e869827ef7408c225be0756a.jpg&quot;&nbsp;/&gt;&lt;/a&gt; <br> &lt;/div&gt; <br>&lt;/p&gt; <br>&lt;p&gt; <br> &lt;span&nbsp;style=&quot;font-family:SimSun;font-size:16px;&quot;&gt;&amp;nbsp;&nbsp;&amp;nbsp;&nbsp;他把鸽子抓在手里仔细一看，顿时让他哭笑不得：鸽子腿上绑了一串炖肉用的调料，不但有葱、姜、蒜，而且还有八角，哈哈，真是全了。这分明是在告诉鸽主：这只鸽子没啥用处，今免费奉还并倒贴佐料，赶紧炖吃了吧！&lt;/span&gt; <br>&lt;/p&gt; <br>&lt;p&gt; <br> &lt;span&nbsp;style=&quot;font-family:SimSun;font-size:16px;&quot;&gt;&amp;nbsp;&nbsp;&amp;nbsp;&nbsp;这正是：幼鸽串门带料还，寓意深刻不用言，主人见状生五味，遍尝酸甜苦辣咸。&lt;/span&gt; <br>&lt;/p&gt; <br>&lt;p&gt; <br> &lt;span&nbsp;style=&quot;font-family:SimSun;font-size:16px;&quot;&gt;&amp;nbsp;&nbsp;&amp;nbsp;&nbsp;养鸽日久难免会有枯燥的感觉，能遇到这样一位拾鸽不昧并且辛辣、幽默、喜欢搞笑的鸽友，也算是给平淡的生活增添一份乐趣。今日分享给大家，以博一笑。&lt;/span&gt; <br>&lt;/p&gt;";

            //string encodeStr1 = WebApi.Common.HtmlHelper.Encode(originStr);
            //string encodeStr2 = WebApi.Common.HtmlHelper.Encode(originStr);

            string encodeStr1 = originStr;
            string encodeStr2 = originStr;

            string decodeStr1 = DDD.WebApi.Common.HtmlHelper.Decode(encodeStr1);
            string decodeStr2 = System.Web.HttpUtility.HtmlDecode(encodeStr2);

            Assert.Equal(decodeStr1, decodeStr2);
        }
    }
}

