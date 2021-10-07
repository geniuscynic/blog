using System;
using System.Text.RegularExpressions;

namespace DoCare.Zkzx.Core.FrameWork.Tool.ToolKit
{
    public static class DateKit
    {
        public static TimeSpan ToTimeSpan(this string value, TimeSpan defaultTimeSpan)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultTimeSpan;
            }

            var format = "%d\\:%h\\:%m\\:%s";

            var pattern = "^(-?)([0-9]*[dD])?([0-9]*[hH])?([0-9]*[mM])?([0-9]*[sS])?$";

            Match m = Regex.Match(value, pattern);

            var test = m.Groups;

            var flag = test[1].Value;
            var day  = test[2].Value.TimeSpanStringToInt(0);
            var hour = test[3].Value.TimeSpanStringToInt(0);
            var minute = test[4].Value.TimeSpanStringToInt(0);
            var second = test[5].Value.TimeSpanStringToInt(0);

            var results = $"{flag}{day}:{hour}:{minute}:{second}";

           
            return TimeSpan.TryParseExact(results, format, null, out var timeSpan)? timeSpan : defaultTimeSpan;

        }

        private static int TimeSpanStringToInt(this string value, int defaultValue)
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
