using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Common
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class TimeHelper
    {
        /// <summary>
        /// 返回输入时间距离现在时间的差，如2小时前，5分钟前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeDiffUntil(DateTime dt)
        {
            return DateDiff(dt,DateTime.Now);
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

        /// <summary>
        /// 将泛型类转为日期格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dateT"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDate<T>(T dateT)
        {
            if (dateT == null) return null;
            DateTime dtValue;
            if (!DateTime.TryParse(dateT.ToString(), out dtValue)) return null;
            return dtValue;
        }

        /// <summary>
        /// 是否为日期格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dateT"></param>
        /// <returns></returns>
        public static bool IsDate<T>(T dateT)
        {
            return DateTime.TryParse(dateT.ToString(),out DateTime dtValue);
        }
    }
}
