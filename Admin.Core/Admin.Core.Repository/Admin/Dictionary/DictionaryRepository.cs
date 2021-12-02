using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class DictionaryRepository : RepositoryBase<DictionaryEntity>, IDictionaryRepository
    {
        public DictionaryRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}