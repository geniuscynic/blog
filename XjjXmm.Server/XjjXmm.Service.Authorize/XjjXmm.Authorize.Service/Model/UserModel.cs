using System;
using System.Collections.Generic;
using DoCare.Zkzx.Core.FrameWork.Tool.DataValidation;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.Authorize.Repository;
using XjjXmm.FrameWork;
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
           var _userRepository = App.ServiceProvider.GetService<UserRepository>();

            if (!(model is AddUserModel userModel)) throw new Exception("类型错误");


            var account = userModel.Account;


            return _userRepository?.FirstOrDefault(t => t.Account == account).Result != null;

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
         /// 所属的role id
         /// </summary>
         public IEnumerable<string> Roles { get; set; }

        /// <summary>
        ///  0 删除， 1 正常状态
        /// </summary>
        //public int Status { get; set; } = 1;


    }


}