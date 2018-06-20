using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Common
{
    public class ResponseHelper
    {
        public static HttpResponseMessage ObjToResponse(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = Newtonsoft.Json.JsonConvert.SerializeObject(obj);              
            }
            HttpResponseMessage result = new HttpResponseMessage
            {
                Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json")
            };
            return result;
        }
    }
}
