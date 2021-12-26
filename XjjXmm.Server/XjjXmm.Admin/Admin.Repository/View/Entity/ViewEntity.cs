using SqlSugar;
using System;
using System.Collections.Generic;

namespace Admin.Repository.View
{
    /// <summary>
    /// 视图管理
    /// </summary>
	[SugarTable("ad_view")]

    public class ViewEntity : EntityFull
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 所属节点
        /// </summary>
		public long ParentId { get; set; }

        // [Navigate(nameof(ParentId))]
        //public List<ViewEntity> Childs { get; set; }

        /// <summary>
        /// 视图命名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 视图名称
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 视图路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 缓存
        /// </summary>
        public bool Cache { get; set; } = true;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}