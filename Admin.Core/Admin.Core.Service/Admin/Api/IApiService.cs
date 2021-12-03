using System.Collections.Generic;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Api.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Api.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Api
{
    /// <summary>
    /// �ӿڷ���
    /// </summary>
    [ProcessLog]
    public interface IApiService
    {
        /// <summary>
        /// ���һ����¼
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiGetOutput> GetAsync(long id);

        /// <summary>
        /// ����б�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<ApiListOutput>> ListAsync(string key);

        /// <summary>
        /// ��÷�ҳ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PageOutput<ApiEntity>> PageAsync(PageInput<ApiEntity> model);

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddAsync(ApiAddInput input);

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ApiUpdateInput input);

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// ��ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(long id);

        /// <summary>
        /// ������ɾ��
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchSoftDeleteAsync(long[] ids);

        /// <summary>
        /// ͬ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SyncAsync(ApiSyncInput input);
    }
}