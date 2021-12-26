using SqlSugar;
using System.Collections.Generic;

namespace Admin.Repository.Organization
{
    /// <summary>
    /// 组织架构
    /// </summary>
	[SugarTable("pe_organization")]
    public class OrganizationEntity : EntityFull
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>

        public long? TenantId { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
		public long ParentId { get; set; }


        //public List<OrganizationEntity> Childs { get; set; }

        /// <summary>
        /// 名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>

        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>

        public string Value { get; set; }

        /// <summary>
        /// 主管Id
        /// </summary>
        public long? PrimaryEmployeeId { get; set; }

        /// <summary>
        /// 主管
        /// </summary>
       // public EmployeeEntity PrimaryEmployee { get; set; }

        /// <summary>
        /// 员工人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 描述
        /// </summary>

        public string Description { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
		public bool Enabled { get; set; } = true;

        /// <summary>
        /// 排序
        /// </summary>
		public int Sort { get; set; }


        //public ICollection<EmployeeEntity> Employees { get; set; }
    }
}