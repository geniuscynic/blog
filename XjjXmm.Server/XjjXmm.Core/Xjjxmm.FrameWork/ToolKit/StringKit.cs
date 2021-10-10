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

        public static int ToInt(this string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch (Exception e)
            {
                
                throw BussinessException.CreateException(ExceptionCode.EmptyOrNullString, e);
            }
        }

       
    }
}
