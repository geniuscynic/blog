using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class DocumentImageRepository : RepositoryBase<DocumentImageEntity>, IDocumentImageRepository
    {
        public DocumentImageRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}