using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.ViewModels
{
    /// <summary>
    /// 添加menu
    /// </summary>
    public class AddMenuViewModel
    {
        /// <summary>
        /// 父menu id
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Menu { get; set; }
    }
}
