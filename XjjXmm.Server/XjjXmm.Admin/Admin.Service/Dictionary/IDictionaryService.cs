
using Admin.Repository.Dictionary;
using Admin.Service.Dictionary.Input;
using Admin.Service.Dictionary.Output;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Dictionary
{
    [ProcessLog]
    public interface IDictionaryService
    {
        Task<DictionaryGetOutput> Get(long id);

        Task<PageOutput<DictionaryListOutput>> Page(PageInput<DictionaryEntity> model);

        Task<bool> Add(DictionaryAddInput input);

        Task<bool> Update(DictionaryUpdateInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);

        Task<bool> BatchSoftDelete(long[] ids);
    }
}