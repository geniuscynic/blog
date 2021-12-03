using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Dictionary.Input;
using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Dictionary
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