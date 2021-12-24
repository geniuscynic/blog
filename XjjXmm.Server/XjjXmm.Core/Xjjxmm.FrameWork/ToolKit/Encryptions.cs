using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XjjXmm.FrameWork.ToolKit {

    /// <summary>
    /// 加密工具类
    /// </summary>
    public sealed class Encryptions {

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">被加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string HMACSHA256(string key, string str) {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] plainBytes = Encoding.UTF8.GetBytes(str);

            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                var sb = new StringBuilder();
                var hashValue = hmacsha256.ComputeHash(plainBytes);
                foreach (byte x in hashValue)
                {
                    sb.Append(string.Format("{0:x2}", x));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">被加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string MD5(string str) {
            //using (MD5 md5 = new MD5CryptoServiceProvider())
            //{
            //    byte[] hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(str));

            //    // Convert the byte array to hexadecimal string
            //    var sb = new StringBuilder();
            //    for (var i = 0; i < hashBytes.Length; i++)
            //    {
            //        sb.Append(hashBytes[i].ToString("X2"));
            //    }
            //    return sb.ToString();
            //}
            return MD5(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">被加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string MD5(byte[] content)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] hashBytes = md5.ComputeHash(content);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (var i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="src">编码源</param>
        /// <returns>编码字符</returns>
        public static string EncodeBase64(string src) {
            return EncodeBase64(Encoding.Default, src);
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="encoding">编码方式</param>
        /// <param name="src">编码源</param>
        /// <returns>编码字符</returns>
        public static string EncodeBase64(Encoding encoding, string src) {
            byte[] bytes = encoding.GetBytes(src);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="src">解码源</param>
        /// <returns>解码字符</returns>
        public static string DecodeBase64(string src) {
            return DecodeBase64(Encoding.Default, src);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="encoding">编码方式</param>
        /// <param name="src">解码源</param>
        /// <returns>解码字符</returns>
        public static string DecodeBase64(Encoding encoding, string src) {
            byte[] bytes = Convert.FromBase64String(src);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 通过DES算法加密一个字符串
        /// </summary>
        /// <param name="key">加密密室</param>
        /// <param name="src">字符串</param>
        /// <returns>加密字符串</returns>
        public static string DesEncrypt(string key, string src)
        {
            return DesEncrypt(key, null, src);
        }

        /// <summary>
        /// 通过DES算法加密一个字符串
        /// </summary>
        /// <param name="key">加密密室</param>
        /// <param name="src">字符串</param>
        /// <returns>加密字符串</returns>
        public static string DesEncrypt(string key, string iv, string src)
        {
            return Encrypt(new DESCryptoServiceProvider()
            {
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            }, key, iv, src);
        }

        /// <summary>
        /// 通过DES算法加密一个字符串
        /// </summary>
        /// <param name="key">加密密室</param>
        /// <param name="src">字符串</param>
        /// <returns>加密字符串</returns>
        public static string DesEncrypt(string key, string iv, string src, CipherMode cipher, PaddingMode padding)
        {
            return Encrypt(new DESCryptoServiceProvider()
            {
                Mode = cipher,
                Padding = padding
            }, key, iv, src);
        }

        /// <summary>
        /// 通过DES算法解密一个字符串
        /// </summary>
        /// <param name="key">解密密室</param>
        /// <param name="src">加密字符串</param>
        /// <returns>解密字符串</returns>
        public static string DesDecrypt(string key, string src)
        {
            return DesDecrypt(key, null, src);
        }

        /// <summary>
        /// 通过DES算法解密一个字符串
        /// </summary>
        /// <param name="key">解密密室</param>
        /// <param name="src">加密字符串</param>
        /// <returns>解密字符串</returns>
        public static string DesDecrypt(string key, string iv, string src)
        {
            return DesDecrypt(key, iv, src, CipherMode.ECB, PaddingMode.PKCS7);
        }

        /// <summary>
        /// 通过DES算法解密一个字符串
        /// </summary>
        /// <param name="key">解密密室</param>
        /// <param name="src">加密字符串</param>
        /// <returns>解密字符串</returns>
        public static string DesDecrypt(string key, string iv, string src, CipherMode cipher, PaddingMode padding)
        {
            return Decrypt(new DESCryptoServiceProvider()
            {
                Mode = cipher,
                Padding = padding
            }, key, iv, src);
        }

        /// <summary>
        /// 通过制定的算法模式来加密一个字符串(不支持中文)
        /// </summary>
        /// <param name="Algorithm">加密的算法</param>
        /// <param name="key">加密Key</param>
        /// <param name="src">将要被加密的值</param>
        private static string Encrypt(SymmetricAlgorithm Algorithm, string key, string iv, string src)
        {
            // 将字符串保存到字节数组中
            byte[] InputBytes = Encoding.UTF8.GetBytes(src);

            // 创建一个key.
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            if (!string.IsNullOrEmpty(iv) && iv.Length >= 8) keyIV = Encoding.UTF8.GetBytes(iv.Substring(0, 8));

            MemoryStream MemStream = new MemoryStream();
            CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);

            // Write the byte array into the crypto stream( It will end up in the memory stream).
            CrypStream.Write(InputBytes, 0, InputBytes.Length);
            CrypStream.FlushFinalBlock();

            // Get the data back from the memory stream, and into a string.
            string result = Convert.ToBase64String(MemStream.ToArray());
            MemStream.Close();
            CrypStream.Close();
            return result;
        }

        /// <summary>
        /// 通过制定的算法模式来加密一个字符串(不支持中文)
        /// </summary>
        /// <param name="Algorithm">解密的算法</param>
        /// <param name="key">解密Key</param>
        /// <param name="src">将要被解密的值</param>
        private static string Decrypt(SymmetricAlgorithm Algorithm, string key, string iv, string src)
        {
            // Put the input string into the byte array.
            byte[] inputBytes = Convert.FromBase64String(src);

            // Create the key.
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            if (!string.IsNullOrEmpty(iv) && iv.Length >= 8) keyIV = Encoding.UTF8.GetBytes(iv.Substring(0, 8));

            MemoryStream MemStream = new MemoryStream();
            CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);

            // Flush the data through the crypto stream into the memory stream.
            CrypStream.Write(inputBytes, 0, inputBytes.Length);
            CrypStream.FlushFinalBlock();

            // Get the decrypted data back from the memory stream.
            string result = Encoding.UTF8.GetString(MemStream.ToArray());
            MemStream.Close();
            CrypStream.Close();
            return result;
        }
    }
}
