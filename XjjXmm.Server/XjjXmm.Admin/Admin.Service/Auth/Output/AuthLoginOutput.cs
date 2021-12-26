namespace Admin.Service.Auth
{
    public class AuthLoginOutput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>

        public string Avatar { get; set; }

        /// <summary>
        /// 租户类型
        /// </summary>
       // public TenantType? TenantType { get; set; }

        /// <summary>
        /// 数据隔离
        /// </summary>
        //public DataIsolationType? DataIsolationType { get; set; }
    }
}