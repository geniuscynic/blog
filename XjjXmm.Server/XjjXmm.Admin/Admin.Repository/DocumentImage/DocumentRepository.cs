using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.DocumentImage
{
    [Injection]
    public class DocumentImageRepository : RepositoryBase<DocumentImageEntity>, IDocumentImageRepository
    {
        public DocumentImageRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}