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
        /// 将String转换为DateTime?类型
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns>DateTime?</returns>
        public static DateTime? StringToDate(string dateString)
        {
            if (String.IsNullOrEmpty(dateString)) return null;
            DateTime dtValue;
            if (!DateTime.TryParse(dateString, out dtValue)) return null;
            return dtValue;
        }

        /// <summary>
        /// 将object转换为DateTime?类型
        /// </summary>
        /// <param name="dateObj"></param>
        /// <returns></returns>
        public static DateTime? ObjectToDate(object dateObj)
        {
            if (dateObj == null) return null;
            DateTime dtValue;
            if (!DateTime.TryParse(dateObj.ToString(), out dtValue)) return null;
            return dtValue;
        }
        
        /// <summary>
        /// 是否为日期格式
        /// Date.Parse(object o)方法接受一个object类型的参数，当参数为空或转换失败时会抛出异常
        /// DateTime.TryParse方法不会抛出异常
        /// </summary>
        /// <param name="dateObj"></param>
        /// <returns></returns>
        public static bool IsDate(object dateObj)
        {
            return DateTime.TryParse(dateObj.ToString(),out DateTime dtValue);
        }


    }
}
