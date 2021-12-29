using Admin.Repository.ApiEntity;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Api
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
        Task<bool> Delete(long id);

        /// <summary>
        /// ��ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDelete(long id);

        /// <summary>
        /// ������ɾ��
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchSoftDelete(long[] ids);

        /// <summary>
        /// ͬ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SyncAsync(ApiSyncInput input);
    }
}