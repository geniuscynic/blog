﻿using System.Collections.Generic;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.View;
using Admin.Core.Service.Admin.View.Input;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.View.Output;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 视图管理
    /// </summary>
    public class ViewController : AreaController
    {
        private readonly IViewService _viewService;

        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }

        /// <summary>
        /// 查询单条视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ViewGetOutput> Get(long id)
        {
            return await _viewService.GetAsync(id);
        }

        /// <summary>
        /// 查询全部视图
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ViewListOutput>> GetList(string key)
        {
            return await _viewService.ListAsync(key);
        }

        /// <summary>
        /// 查询分页视图
        /// </summary>
        /// <param name="model">分页模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<ViewEntity>> GetPage(PageInput<ViewEntity> model)
        {
            return await _viewService.PageAsync(model);
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(ViewAddInput input)
        {
            return await _viewService.AddAsync(input);
        }

        /// <summary>
        /// 修改视图
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(ViewUpdateInput input)
        {
            return await _viewService.UpdateAsync(input);
        }

        /// <summary>
        /// 删除视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _viewService.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量删除视图
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _viewService.BatchSoftDeleteAsync(ids);
        }

        /// <summary>
        /// 同步视图
        /// 支持新增和修改视图
        /// 根据视图是否存在自动禁用和启用视图
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Sync(ViewSyncInput input)
        {
            return await _viewService.SyncAsync(input);
        }
    }
}