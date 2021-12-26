using SqlSugar;

namespace Admin.Repository.EmployeeOrganization
{
    /// <summary>
    /// 员工附属部门
    /// </summary>
	[SugarTable("ad_employee_organization")]

    public class EmployeeOrganizationEntity : EntityAdd
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
		public long EmployeeId { get; set; }

        /// <summary>
        /// 员工
        /// </summary>
        //public EmployeeEntity Employee { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
		public long OrganizationId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        //public OrganizationEntity Organization { get; set; }
    }
}