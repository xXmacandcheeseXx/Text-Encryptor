using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Text_File_Encryptor
{
    class Encrypt
    {

        public string EncryptText(string text, string key1, string key2)
        {
            string EncryptedText = "";
            byte[] secretkeyByte = { };
            secretkeyByte = Encoding.UTF8.GetBytes(key1);
            byte[] publickeybyte = { };
            publickeybyte = Encoding.UTF8.GetBytes(key2);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            byte[] inputbyteArray = Encoding.UTF8.GetBytes(text);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                EncryptedText = Convert.ToBase64String(ms.ToArray());
            }
            return EncryptedText;
        }

        public string DecryptText(string text, string key1, string key2)
        {
            string DecryptedFileText = "";
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.Encoding.UTF8.GetBytes(key1);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(key2);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[text.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(text.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                try
                {
                    cs.FlushFinalBlock();
                }
                catch (CryptographicException)
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect password and/or secret");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Encoding encoding = Encoding.UTF8;
                DecryptedFileText = encoding.GetString(ms.ToArray());
            }
            return DecryptedFileText;
        }
    }
}