

using SqlSugar;

namespace Admin.Repository.DictionaryType
{
    /// <summary>
    /// 数据字典类型
    /// </summary>
	[SugarTable("ad_dictionary_type")]

    public class DictionaryTypeEntity : EntityFull
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>

        public long? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        
        public string Code { get; set; }

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
    }
}