using Identity.Service.Tools.Crypto;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task EncryptTest()
        {
            string key = "Fteam-Identity-1";
            string text = "hello how are you";

            var encData = CryptoEngine.Encrypt(text, key);
            var decData = CryptoEngine.Decrypt(encData, key);
            if (decData == text)
                Assert.Pass(decData);
            else
                Assert.Fail(decData);
        }
    }
}