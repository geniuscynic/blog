﻿using Blog.Core.Models;
using Blog.Core.VeiwModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    /// <summary>
    /// user service
    /// </summary>
    public interface IUserService  : IBaseService<User>
    {
        /// <summary>
        /// 登入验证
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        Task<string> Login(LoginViewModel loginViewModel);
    }
}