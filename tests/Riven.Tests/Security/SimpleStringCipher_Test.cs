using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;


namespace Riven.Security
{
   public class SimpleStringCipher_Test
    {
        [Fact]
        public void EncryptAndDecrypt()
        {
            var passPhrase = "I`m staneee.";
            var salt = Encoding.UTF8.GetBytes("staneee salt");


            var str = "123456";

            var encrypt = SimpleStringCipher.Instance.Encrypt(str);
            SimpleStringCipher.Instance.Decrypt(encrypt).ShouldBe(str);



            encrypt = SimpleStringCipher.Instance.Encrypt(str, passPhrase, salt);
            SimpleStringCipher.Instance.Decrypt(encrypt, passPhrase, salt).ShouldBe(str);

        }
    }
}
