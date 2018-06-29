using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Linq;

namespace DDD.Common
{
    public class JsonHelper
    {
        public static string ObjectToJson<T>(T t)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(t);
            return json;
        }

        public static T JsonToObject<T>(string json)
        {
            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return t;
        }

        public static Dictionary<string, string> JsonToDic(string json)
        {
            var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return dic;
        }
    }
}
