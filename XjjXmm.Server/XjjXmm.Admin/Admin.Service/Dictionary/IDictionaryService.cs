
using Admin.Service.Dictionary.Input;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Dictionary
{
    [ProcessLog]
    public interface IDictionaryService
    {
        Task<IResponseOutput> GetAsync(long id);

        Task<IResponseOutput> PageAsync(PageInput<DictionaryEntity> model);

        Task<IResponseOutput> AddAsync(DictionaryAddInput input);

        Task<IResponseOutput> UpdateAsync(DictionaryUpdateInput input);

        Task<IResponseOutput> DeleteAsync(long id);

        Task<IResponseOutput> SoftDeleteAsync(long id);

        Task<IResponseOutput> BatchSoftDeleteAsync(long[] ids);
    }
}