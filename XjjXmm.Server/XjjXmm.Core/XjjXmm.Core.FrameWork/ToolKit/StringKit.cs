using System;
using XjjXmm.Core.FrameWork.Common;

namespace XjjXmm.Core.FrameWork.ToolKit
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
                
                throw new BussinessException(new BussinessException.ErrCode()
                {

                });
            }
        }
    }
}
