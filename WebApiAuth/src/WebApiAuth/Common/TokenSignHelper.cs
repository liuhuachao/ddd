using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuth.Models;
using WebApiAuth.Enums;

namespace WebApiAuth.Common
{
    public class TokenSignHelper
    {
        protected bool IsTimestampValid(string requestTimeStamp)
        {
            DateTime startTs = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan currentTs = DateTime.UtcNow - startTs;            
            double serverTotalSeconds = Convert.ToUInt64(currentTs.TotalSeconds);           
            bool isdouble = double.TryParse(requestTimeStamp, out double requestTotalSeconds);
            bool istimeout = serverTotalSeconds - requestTotalSeconds > 300;
            if (!isdouble||istimeout)
            {
                   
            }
            return false;
        }
    }
}
