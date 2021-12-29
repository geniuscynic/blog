using Admin.Repository.Document;

namespace Admin.Service.Document.Input
{
    public class DocumentAddMenuInput
    {
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public DocumentType Type { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// ˵��
        /// </summary>
        public string Description { get; set; }
    }
}