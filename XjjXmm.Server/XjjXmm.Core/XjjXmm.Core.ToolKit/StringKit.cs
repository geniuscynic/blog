using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Core.ToolKit
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
                
                throw new BussinessException(new E);
            }
        }
    }
}
