namespace Admin.Service.Document.Input
{
    public class DocumentAddImageInput
    {
        /// <summary>
        /// �û�Id
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// ����·��
        /// </summary>
        public string Url { get; set; }
    }
}