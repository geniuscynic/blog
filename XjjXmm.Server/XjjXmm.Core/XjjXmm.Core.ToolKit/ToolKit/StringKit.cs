using System;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;

namespace DoCare.Zkzx.Core.FrameWork.Tool.ToolKit
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
