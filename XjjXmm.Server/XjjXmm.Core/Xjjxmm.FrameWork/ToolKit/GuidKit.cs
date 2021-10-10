using System;

namespace XjjXmm.FrameWork.ToolKit {

    /// <summary>
    /// GUID工具类
    /// </summary>
    public sealed class GuidKit {

        /// <summary>
        /// 获取GUID字符
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="uppercase">是否大写</param>
        /// <returns>GUID字符</returns>
        public static string Get(GuidFormat format, bool uppercase = false) {
            string guid = string.Empty;
            switch (format) {
                case GuidFormat.NoHyphen:
                    guid = Guid.NewGuid().ToString("N");
                    break;

                case GuidFormat.Brace:
                    guid = Guid.NewGuid().ToString("B");
                    break;

                case GuidFormat.Parentheses:
                    guid = Guid.NewGuid().ToString("P");
                    break;
                default:
                    guid = Guid.NewGuid().ToString("D");
                    break;
            }
            return !uppercase ? guid : guid.ToUpper();
        }


        /// <summary>
        /// 获取GUID字符
        /// </summary>
        /// <param name="uppercase">是否大写</param>
        /// <returns>GUID字符</returns>
        public static string Get(bool uppercase = false) {
            return Get(GuidFormat.NoHyphen, uppercase);
        }

    }

    /// <summary>
    /// Guid格式
    /// </summary>
    public enum GuidFormat {
        /// <summary>
        /// 默认格式
        /// </summary>
        Default,
        /// <summary>
        /// 没有短横线
        /// </summary>
        NoHyphen,
        /// <summary>
        /// 大括号包围
        /// </summary>
        Brace,
        /// <summary>
        /// 小括号包围
        /// </summary>
        Parentheses
    }
}
