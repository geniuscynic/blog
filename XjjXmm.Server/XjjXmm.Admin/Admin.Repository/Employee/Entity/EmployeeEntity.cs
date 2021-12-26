using Admin.Repository.Organization;
using Admin.Repository.Position;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Admin.Repository.Employee
{
    /// <summary>
    /// 员工
    /// </summary>
	[SugarTable("pe_employee")]

    public class EmployeeEntity : EntityFull
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>

        public long? TenantId { get; set; }

        //public TenantEntity Tenant { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        //public UserEntity User { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>

        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex? Sex { get; set; }

        /// <summary>
        /// 工号
        /// </summary>

        public string Code { get; set; }

        /// <summary>
        /// 主属部门Id
        /// </summary>
        public long OrganizationId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public OrganizationEntity Organization { get; set; }

        /// <summary>
        /// 主管Id
        /// </summary>
        public long? PrimaryEmployeeId { get; set; }

        /// <summary>
        /// 主管
        /// </summary>
        //public EmployeeEntity PrimaryEmployee { get; set; }

        /// <summary>
        /// 职位Id
        /// </summary>
        public long PositionId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public PositionEntity Position { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>

        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime? EntryTime { get; set; }


        //public ICollection<OrganizationEntity> Organizations { get; set; }
    }
}