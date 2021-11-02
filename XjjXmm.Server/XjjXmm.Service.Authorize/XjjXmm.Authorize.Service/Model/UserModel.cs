using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.Authorize.Repository;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.DataValidation;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Service.Model
{
    

    /// <summary>
    /// 添加用户的view model
    /// </summary>
    public class AddUserModel
    {

        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        [SameUserValidateForNew]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }

        
        /// <summary>
        /// 所属角色
        /// </summary>
        public List<string> Roles { get; set; }
    }

    /// <summary>
    /// 修改类
    /// </summary>
    public class EditUserModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        //[Required]
        //public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
       // [Required]
       // public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }


        /// <summary>
        /// 所属角色
        /// </summary>
        public List<string> Roles { get; set; }
    }
    
    //[Injection]
    public class SameUserValidateForNew : AbstractValidator
    {
        //private readonly UserRepository _userRepository;
        //public IUserRepository UserRepository { get; set; }

        public SameUserValidateForNew()
        {
            //_userRepository = App.ServiceProvider.GetService<UserRepository>();
        }

       
       

        public override string CustomMessage { get; set; } = "账号已经存在 ";

        public override bool IsValid(object value, object model)
        {
            //var _userRepository = App.ServiceProvider.GetService<UserRepository>();

            // if (!(model is AddUserModel userModel)) throw new Exception("类型错误");


            // var account = userModel.Account;


            // return _userRepository?.FirstOrDefault(t => t.Account == account).Result == null;
            throw new NotImplementedException();

        }
    }

   

    /// <summary>
    /// 查询返回
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 最后登入时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdatedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdatedBy { get; set; }


        /// <summary>
        ///  0 删除， 1 正常状态
        /// </summary>
        //public int Status { get; set; } = 1;
    }

     /// <summary>
     /// 详细信息
     /// </summary>
     public class UserDetailModel
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
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

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
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否为admin账号
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }



        /// <summary>
        /// 最后修改密码的时间
        /// </summary>
        public DateTime PwdResetTime { get; set; }
    }


}