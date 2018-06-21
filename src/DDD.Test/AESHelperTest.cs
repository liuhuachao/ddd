using System;
using Xunit;
using DDD.Common;

namespace DDD.Test
{
    public class AESHelperTest
    {
        [Fact]
        public void TestEncryptAndDecrypt()
        {
            string originStr = Guid.NewGuid().ToString();
            string secretKey = Guid.NewGuid().ToString("N");
            string encryptStr = AESHelper.AESEncrypt(originStr, secretKey);
            string decryptStr = AESHelper.AESDecrypt(encryptStr,secretKey);
            Assert.Equal(decryptStr,originStr);
        }

        [Fact]
        public void TestEncryptAndDecrypt2()
        {
            string originStr =  " Server=data.chsgw.com;Database=AreWeb_Pigeons;User ID=sa;password=Chsgw@chsgw.com;Persist Security Info=True;";
            string encryptStr = AESHelper.AESEncrypt(originStr);
            string decryptStr = AESHelper.AESDecrypt(encryptStr);
            Assert.Equal(originStr, originStr);
        }
    }
}

