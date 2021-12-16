using System;
using Course;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class VigenereCipherTest
    {
        //Encrypt tests
        [TestMethod]
        public void EncryptTest1()
        {
            string mess = "Си шарп – это весело!";
            string key = "стекло";
            var result = VigenereCipher.Crypt(mess, key, CryptMode.Encrypt);
            Assert.AreEqual(result, "Гы экью – оеу мрацюу!");
        }

        [TestMethod]
        public void EncryptTest2()
        {
            string mess = "А C double plus – нет!";
            string key = "стекло";
            var result = VigenereCipher.Crypt(mess, key, CryptMode.Encrypt);
            Assert.AreEqual(result, "С C double plus – айэ!");
        }

        [TestMethod]
        public void EncryptTest3()
        {
            string mess = "Мой текст";
            string key = "точувствокогдаутебяслишкомкороткий";
            var result = VigenereCipher.Crypt(mess, key, CryptMode.Encrypt);
            Assert.AreEqual(result, "Яэб ёжьдф");
        }

        //Decrypt tests
        [TestMethod]
        public void DecryptTest1()
        {
            string mess = "Гы экью – оеу мрацюу!";
            string key = "стекло";
            var result = VigenereCipher.Crypt(mess, key, CryptMode.Decrypt);
            Assert.AreEqual(result, "Си шарп – это весело!");
        }

        [TestMethod]
        public void DecryptTest2()
        {
            string mess = "С C double plus – айэ!";
            string key = "стекло";
            var result = VigenereCipher.Crypt(mess, key, CryptMode.Decrypt);
            Assert.AreEqual(result, "А C double plus – нет!");
        }

        [TestMethod]
        public void DecryptTest3()
        {
            string mess = "Яэб ёжьдф";
            string key = "точувствокогдаутебяслишкомкороткий";
            var result = VigenereCipher.Crypt(mess, key, CryptMode.Decrypt);
            Assert.AreEqual(result, "Мой текст");
        }
    }
}
