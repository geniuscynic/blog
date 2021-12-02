using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class DictionaryTypeRepository : RepositoryBase<DictionaryTypeEntity>, IDictionaryTypeRepository
    {
        public DictionaryTypeRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}