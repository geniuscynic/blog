
using SqlSugar;

namespace Admin.Repository.Document
{
    /// <summary>
    /// 文档
    /// </summary>
	[SugarTable( "ad_document")]
    
    public class DocumentEntity : EntityFull
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>

        public long? TenantId { get; set; }

        /// <summary>
        /// 父级节点
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        
        public string Label { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        
        //public DocumentType Type { get; set; }

        /// <summary>
        /// 命名
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        
        public string Content { get; set; }

        /// <summary>
        /// Html
        /// </summary>
        
        public string Html { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
		public bool Enabled { get; set; } = true;

        /// <summary>
        /// 打开组
        /// </summary>
        public bool? Opened { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; } = 0;

        /// <summary>
        /// 描述
        /// </summary>
        
        public string Description { get; set; }
    }
}