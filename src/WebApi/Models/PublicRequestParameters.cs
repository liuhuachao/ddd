using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class PublicRequestParameters
    {
        /// <summary>
        /// Unix时间戳
        /// </summary>
        public int Timestamp { get; set; }

        /// <summary>
        /// 安全的强随机数
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 临时安全凭证
        /// </summary>
        public Guid Token { get; set; }


        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecretId { get; set; }

        /// <summary>
        /// 安全签名
        /// </summary>
        public string signature { get; set; }

    }
}
