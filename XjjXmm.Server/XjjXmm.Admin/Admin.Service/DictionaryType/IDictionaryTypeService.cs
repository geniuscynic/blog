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
        Task<DictionaryTypeGetOutput> Get(long id);

        Task<PageOutput<DictionaryTypeListOutput>> Page(PageInput<DictionaryTypeEntity> model);

        Task<bool> Add(DictionaryTypeAddInput input);

        Task<bool> Update(DictionaryTypeUpdateInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);

        Task<bool> BatchSoftDelete(long[] ids);
    }
}