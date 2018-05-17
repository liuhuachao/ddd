using System;
using Xunit;

namespace WebApiXUnitTest
{
    public class AESUnitTest
    {
        [Fact]
        public void TestEncryptAndDecrypt()
        {
            string originStr = Guid.NewGuid().ToString();
            string secretKey = Guid.NewGuid().ToString("N");
            string encryptStr = WebApi.Common.AESHelper.AESEncrypt(originStr, secretKey);
            string decryptStr = WebApi.Common.AESHelper.AESDecrypt(encryptStr,secretKey);
            Assert.Equal(decryptStr,originStr);
        }
    }
}

