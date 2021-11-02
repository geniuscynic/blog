using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Model
{
    public class RoleSmallDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据权限，全部 、 本级 、 自定义
        /// </summary>
        public string DataScope { get; set; }


        /// <summary>
        /// 级别，数值越小，级别越大
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        //public string Description { get; set; }
    }
}
