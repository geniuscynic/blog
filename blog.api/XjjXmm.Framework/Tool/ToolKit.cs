using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Framework.Tool
{
    public static class ToolKit
    {
        public static int TimeSpanStringToInt(this string value, int defaultValue)
        {
            value = value.ToLower()
                .Replace("d", "")
                .Replace("h", "")
                .Replace("m", "")
                .Replace("s", "");

            return int.TryParse(value, out var outValue) ? outValue : defaultValue;
        }
    }
}
