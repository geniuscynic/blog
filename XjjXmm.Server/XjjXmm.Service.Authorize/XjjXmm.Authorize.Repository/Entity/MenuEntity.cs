using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    [Table("sys_menu")]
    public class MenuEntity : BaseEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true, ColumnName = "menu_id", IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        /// <param name=""></param>
        [Column(ColumnName = "title")]
        public string Title { get; set; }

        /// <summary>
        ///  菜单组件名称
        /// </summary>
        [Column(ColumnName = "name")]
        public string ComponentName { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        [Column(ColumnName = "menuSort")]
        public int MenuSort { get; set; } = 999;

        /// <summary>
        ///  组件路径
        /// </summary>
        [Column(ColumnName = "menuSort")]
        public string Component { get; set; }

        /// <summary>
        ///  路由地址
        /// </summary>
        [Column(ColumnName = "path")]
        public string Path { get; set; }


        /// <summary>
        ///  菜单类型，目录、菜单、按钮
        /// </summary>
        [Column(ColumnName = "type")]
        public int Type { get; set; }

        /// <summary>
        ///  权限标识
        /// </summary>
        [Column(ColumnName = "permission")]
        public string Permission { get; set; }

        /// <summary>
        ///  菜单图标
        /// </summary>
        [Column(ColumnName = "icon")]
        public string Icon { get; set; }

        /// <summary>
        ///  缓存
        /// </summary>
        [Column(ColumnName = "cache")]
        public bool Cache { get; set; }

        /// <summary>
        ///  是否隐藏
        /// </summary>
        [Column(ColumnName = "cache")]
        public bool Hidden { get; set; }

        /// <summary>
        ///  上级菜单
        /// </summary>
        [Column(ColumnName = "pid")]
        public long Pid { get; set; }

        /// <summary>
        ///  子节点数目
        /// </summary>
        [Column(ColumnName = "subCount")]
        public int SubCount { get; set; }

        /// <summary>
        ///  外链菜单
        /// </summary>
        [Column(ColumnName = "iFrame")]
        public bool Frame { get; set; }


    }
}
