using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Common
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class TimeHelper
    {
        /// <summary>
        /// 返回输入时间距离现在UTC时间的差，如2小时前，5分钟前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeDiff(DateTime dt)
        {
            return DateDiff(dt,DateTime.UtcNow);
        }

        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime dt1, DateTime dt2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = dt2 - dt1;
                if (ts.Days >= 1)
                {
                    dateDiff = dt1.Month.ToString() + "月" + dt1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }

    }
}
