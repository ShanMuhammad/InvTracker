using InvTracker.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InvTracker.Infrastructure
{

    public static class EncryptDecryptClass
    {
        private static string EncryptionKey = BasicSettings.GetAppSettingsValue("EncryptionKey");
        public static string EncryptData(string clearText)
        {
            if (string.IsNullOrEmpty(clearText)) { return string.Empty; }
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
            return clearText;
        }

        public static string DecryptData(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) { return string.Empty; }
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText.Replace(" ", "+"));
                //  byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
            return cipherText;
        }
    }

    public static class EncodeDecodeQueryString
    {
        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("EY123456");

        public static string EncodeQueryString(string clearText)
        {
            if (string.IsNullOrEmpty(clearText)) { return string.Empty; }
            try
            {
                clearText = HttpUtility.UrlEncode(EncryptQueryString(clearText));
            }
            catch (Exception ex)
            { throw ex; }
            //%2f : "/", %2B : +, %26 : &
            return RemoveSpecialCharacterFromURL(clearText, true);
        }

        public static string DecodeQueryString(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) { return string.Empty; }
            cipherText = RemoveSpecialCharacterFromURL(cipherText, false);
            try
            {
                cipherText = DecryptQueryString(HttpUtility.UrlDecode(cipherText));
            }
            catch (Exception ex)
            { throw ex; }
            return cipherText;
        }

        private static string RemoveSpecialCharacterFromURL(string queryString, bool replaceFromClearText)
        {
            if (replaceFromClearText)
            {
                queryString = queryString.Replace("%2F", "ErnstYoung1").Replace("%2f", "ErnstYoung1").Replace("%2B", "ErnstYoung2").Replace("%2b", "ErnstYoung2").Replace("%3D", "ErnstYoung3").Replace("%3d", "ErnstYoung3").Replace("%26", "ErnstYoung10");
            }
            else
            {
                queryString = queryString.Replace("ErnstYoung1", "%2F").Replace("ErnstYoung1", "%2f").Replace("ErnstYoung2", "%2B").Replace("ErnstYoung2", "%2b").Replace("ErnstYoung3", "%3D").Replace("%3d", "ErnstYoung3").Replace("ErnstYoung10", "%26");
            }
            return queryString;
        }

        private static string EncryptQueryString(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                return string.Empty;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string DecryptQueryString(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                return string.Empty;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

    }
}

