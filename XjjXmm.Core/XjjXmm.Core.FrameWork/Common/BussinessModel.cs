﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Extension.Common
{
   
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class BussinessModel<T>
    {

        public BussinessModel(T response)
        {
            this.response = response;
        }
        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; } = 200;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = true;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; } = "";
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T response { get; set; }

    }
}
