﻿using System;
using System.Collections.Generic;

namespace Permission.Model
{
    /// <summary>
    /// 添加用户的view model
    /// </summary>
    public class AddUserModel
    {

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

        

        //todo 要做权限判断，只有管理员才能设置初始role
        /// <summary>
        /// 所属角色
        /// </summary>
        public List<int> Roles { get; set; }
    }

    /// <summary>
    /// 修改类
    /// </summary>
    public class EditUserModel
    {
        /// <summary>
        /// 主键ID
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



        //todo 要做权限判断，只有管理员才能设置初始role
        /// <summary>
        /// 所属角色
        /// </summary>
        public List<int> Roles { get; set; }
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
}