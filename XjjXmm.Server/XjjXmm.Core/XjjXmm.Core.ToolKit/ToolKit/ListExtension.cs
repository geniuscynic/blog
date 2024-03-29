﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoCare.Zkzx.Core.FrameWork.Tool.ToolKit
{
    public static class ListKit
    {
        public static string Concat(this List<string> src, string separator = ",", string closure = "", bool needWrap = false)
        {
            if (src == null || !src.Any()) return string.Empty;


            StringBuilder sb = new StringBuilder();



            foreach (var s in src)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(closure).Append(s).Append(closure);
            }

            return needWrap ? $"{separator}{sb}{separator}" : sb.ToString();
        }


    }
}
