using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.FrameWork.ToolKit.DataEncryption.Encryptions
{
    public class BCryptPasswordEncoder
    {
        /// <summary>
        /// 字符串 BCryptPassword 比较
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="hash">MD5 字符串</param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns>bool</returns>
        public static bool Compare(string text, string password)
        {
            var hash = Encrypt(password);
           return BCrypt.Net.BCrypt.Verify(text, hash);
        }

        /// <summary>
        /// BCryptPassword 加密
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }
    }
}
