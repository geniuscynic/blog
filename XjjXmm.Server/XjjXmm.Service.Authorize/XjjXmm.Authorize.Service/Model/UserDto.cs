using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Model
{
    public class UserDto
    {
        /// <summary>
        ///  ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DeptId { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// 头像真实名称
        /// </summary>
        public string AvatarName { get; set; }

        /// <summary>
        /// 头像存储的路径
        /// </summary>
        public string AvatarPath { get; set; }


        /// <summary>
        /// 是否为admin账号
        /// </summary>
        public bool IsAdmin { get; set; }


        /// <summary>
        /// 最后修改密码的时间
        /// </summary>
        public DateTime? PwdResetTime { get; set; }


        public List<RoleSmallDto> Roles { get; set; } = new List<RoleSmallDto>();

        public List<DeptSmallModel> Dept { get; set; } = new List<DeptSmallModel>();
    }
}
