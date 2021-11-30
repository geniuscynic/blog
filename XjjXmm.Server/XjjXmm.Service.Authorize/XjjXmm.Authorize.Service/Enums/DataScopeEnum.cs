using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Enums
{
    public class JavaeEnum
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public class DataScopeEnum
    {
        public const string ALL = "ALL";
        public const string THIS_LEVEL = "THIS_LEVEL";
        public const string CUSTOMIZE = "CUSTOMIZE";

        private static List<JavaeEnum> Enums = new List<JavaeEnum>()
        {
            new JavaeEnum
            {
                Key = ALL,
                Value = "全部",
                Description = "全部的数据权限"
            },
            new JavaeEnum
            {
                Key = THIS_LEVEL,
                Value = "本级",
                Description = "自己部门的数据权限"
            },
            new JavaeEnum
            {
                Key = CUSTOMIZE,
                Value = "自定义",
                Description = "自定义的数据权限"
            },
        };

        public static JavaeEnum Find(string val)
        {
            return Enums.FirstOrDefault(t => t.Value == val);

        }
    }
}
