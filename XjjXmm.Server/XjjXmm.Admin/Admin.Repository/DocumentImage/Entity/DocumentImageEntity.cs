using SqlSugar;

namespace Admin.Repository.DocumentImage
{
    /// <summary>
    /// 文档图片
    /// </summary>
	[SugarTable("ad_document_image")]
    public class DocumentImageEntity : EntityAdd
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 文档Id
        /// </summary>
        public long DocumentId { get; set; }

        //public DocumentEntity Document { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string Url { get; set; }
    }
}