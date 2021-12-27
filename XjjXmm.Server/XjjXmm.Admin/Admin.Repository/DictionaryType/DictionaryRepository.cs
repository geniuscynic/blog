using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.DictionaryType
{
    [Injection]
    public class DictionaryTypeRepository : RepositoryBase<DictionaryTypeEntity>, IDictionaryTypeRepository
    {
        public DictionaryTypeRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}