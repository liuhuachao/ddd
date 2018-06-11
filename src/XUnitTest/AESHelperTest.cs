using System;
using Xunit;

namespace WebApiTest
{
    public class AESHelperTest
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

        [Fact]
        public void TestEncryptAndDecrypt2()
        {
            string originStr =  " Server=data.chsgw.com;Database=AreWeb_Pigeons;User ID=sa;password=Chsgw@chsgw.com;Persist Security Info=True;";
            string encryptStr = WebApi.Common.AESHelper.AESEncrypt(originStr);
            string decryptStr = WebApi.Common.AESHelper.AESDecrypt(encryptStr);
            Assert.Equal(originStr, originStr);
        }
    }
}

