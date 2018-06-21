using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Common
{
    /// <summary>
    /// 通用类
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 根据栏目编码获取栏目名称
        /// </summary>
        /// <param name="classCode"></param>
        /// <returns></returns>
        public static string GetClassName(string classCode)
        {
            string className = string.Empty;
            switch (classCode)
            {
                case "010101":
                    className = "国内鸽讯";
                    break;
                case "010105":
                    className = "国际鸽讯";
                    break;
                case "010801":
                case "010802":
                case "010803":
                case "010804":
                case "010805":
                case "010806":
                case "010807": 
                    className = "信鸽知识";
                    break;
            }
            return className;
        }
    }
}
