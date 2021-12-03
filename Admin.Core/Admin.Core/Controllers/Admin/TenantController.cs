using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Tenant;
using Admin.Core.Service.Admin.Tenant.Input;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Tenant.Output;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 租户管理
    /// </summary>
    public class TenantController : AreaController
    {
        private readonly ITenantService _tenantServices;

        public TenantController(ITenantService tenantService)
        {
            _tenantServices = tenantService;
        }

        /// <summary>
        /// 查询单条租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TenantGetOutput> Get(long id)
        {
            return await _tenantServices.GetAsync(id);
        }

        /// <summary>
        /// 查询分页租户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<TenantListOutput>> GetPage(PageInput<TenantEntity> model)
        {
            return await _tenantServices.PageAsync(model);
        }

        /// <summary>
        /// 新增租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(TenantAddInput input)
        {
            return await _tenantServices.AddAsync(input);
        }

        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(TenantUpdateInput input)
        {
            return await _tenantServices.UpdateAsync(input);
        }

        /// <summary>
        /// 彻底删除租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete(long id)
        {
            return await _tenantServices.DeleteAsync(id);
        }

        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _tenantServices.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量删除租户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _tenantServices.BatchSoftDeleteAsync(ids);
        }
    }
}