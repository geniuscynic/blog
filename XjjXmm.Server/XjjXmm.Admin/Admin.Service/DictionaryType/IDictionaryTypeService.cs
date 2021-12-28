using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;
using Admin.Service.DictionaryType.Input;
using Admin.Service.DictionaryType.Output;
using XjjXmm.FrameWork.Common;
using Admin.Repository.DictionaryType;

namespace Admin.Service.DictionaryType
{
    [ProcessLog]
    public partial interface IDictionaryTypeService
    {
        Task<DictionaryTypeGetOutput> GetAsync(long id);

        Task<PageOutput<DictionaryTypeListOutput>> PageAsync(PageInput<DictionaryTypeEntity> model);

        Task<bool> AddAsync(DictionaryTypeAddInput input);

        Task<bool> UpdateAsync(DictionaryTypeUpdateInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> BatchSoftDeleteAsync(long[] ids);
    }
}