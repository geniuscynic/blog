using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class DocumentRepository : RepositoryBase<DocumentEntity>, IDocumentRepository
    {
        public DocumentRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}