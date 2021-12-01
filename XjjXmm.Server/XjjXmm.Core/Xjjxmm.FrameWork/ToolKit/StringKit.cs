using System;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.FrameWork.ToolKit
{
    class StringKit
    {

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
