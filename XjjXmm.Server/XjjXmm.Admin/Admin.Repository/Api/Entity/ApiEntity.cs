using SqlSugar;
using System;
using System.Collections.Generic;

namespace Admin.Repository.Api.Entity
{
    /// <summary>
    /// 接口管理
    /// </summary>
	[SugarTable("ad_api")]

    public class ApiEntity : EntityFull
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
		public long ParentId { get; set; }


        // public List<ApiEntity> Childs { get; set; }

        /// <summary>
        /// 接口命名
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>

        public string Label { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>

        public string Path { get; set; }

        /// <summary>
        /// 接口提交方法
        /// </summary>

        public string HttpMethods { get; set; }

        /// <summary>
        /// 说明
        /// </summary>

        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; } = true;


        // public ICollection<PermissionEntity> Permissions { get; set; }
    }
}