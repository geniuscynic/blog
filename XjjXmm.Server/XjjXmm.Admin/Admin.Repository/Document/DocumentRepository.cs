using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Document
{
    [Injection]
    public class DocumentRepository : RepositoryBase<DocumentEntity>, IDocumentRepository
    {
        public DocumentRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}