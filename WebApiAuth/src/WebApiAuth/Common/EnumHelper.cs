using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace WebApiAuth.Common
{
    public class EnumHelper
    {
        /// <summary>  
        /// 获取枚举类子项描述信息
        /// </summary>  
        /// <param name="enumSubitem">枚举类子项</param>          
        public static string GetEnumDescription(Enum enumSubitem)
        {
            string strValue = enumSubitem.ToString();
            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return strValue;
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)objs[0];
                return da.Description;
            }

        }
    }
}
