using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.DictionaryType.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.DictionaryType.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.DictionaryType
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