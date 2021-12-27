using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Dictionary
{
    [Injection]
    public class DictionaryRepository : RepositoryBase<DictionaryEntity>, IDictionaryRepository
    {
        public DictionaryRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}