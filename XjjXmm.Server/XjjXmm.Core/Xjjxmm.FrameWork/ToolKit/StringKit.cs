using System;
using System.Text;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.FrameWork.ToolKit
{
   public class StringKit
    {
        private static readonly char[] _constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 生成随机字符串，默认32位
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns></returns>
        public static string GenerateRandom(int length = 32)
        {
            var newRandom = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(_constant[rd.Next(_constant.Length)]);
            }
            return newRandom.ToString();
        }
    }

    public static class StringExtension
    {
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }


    }
}
