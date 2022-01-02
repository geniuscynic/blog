using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using Admin.Service.View.Input;
using Admin.Service.View.Output;
using Admin.Repository.View;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.View
{
    [Injection]
    public class ViewService : BaseService, IViewService
    {
        private readonly IViewRepository _viewRepository;

        public ViewService(IViewRepository moduleRepository)
        {
            _viewRepository = moduleRepository;
        }

        public async Task<ViewGetOutput> Get(long id)
        {
            var result = await _viewRepository.GetById(id);

            var dto = result.MapTo<ViewEntity, ViewGetOutput>();
            return dto;
        }

        public async Task<List<ViewListOutput>> List(string key)
        {
            //var data = await _viewRepository
            //    .WhereIf(key.NotNull(), a => a.Path.Contains(key) || a.Label.Contains(key))
            //    .OrderBy(a => a.ParentId)
            //    .OrderBy(a => a.Sort)
            //    .ToListAsync<ViewListOutput>();

            var data = await _viewRepository.List(key);

            return data.MapTo<ViewEntity, ViewListOutput>().ToList();

            //throw new NotImplementedException();
        }

        public async Task<PageOutput<ViewEntity>> Page(PageInput<ViewPageInput> input)
        {
            //var key = input.Filter?.Label;

            //long total;
            //var list = await _viewRepository.Select
            //.WhereIf(key.NotNull(), a => a.Path.Contains(key) || a.Label.Contains(key))
            //.Count(out total)
            //.OrderByDescending(true, c => c.Id)
            //.Page(input.CurrentPage, input.PageSize)
            //.ToList();

            //var data = new PageOutput<ViewEntity>()
            //{
            //    List = list,
            //    Total = total
            //};

            //return data;
            var inputDto = input.MapTo<PageInput<ViewPageInput>, PageInput<ViewEntity>>();
            var result = await _viewRepository.QueryPage(inputDto);
            return new PageOutput<ViewEntity>
            {
                CurrentPage = result.CurrentPage,
                Total = result.Total,
                PageSize = result.PageSize,
                Data = result.Data.MapTo<ViewEntity, ViewEntity>()
            };
        }

        public async Task<bool> Add(ViewAddInput input)
        {
            //var entity = Mapper.Map<ViewEntity>(input);
            //var id = (await _viewRepository.Insert(entity)).Id;

            //return id > 0;

            throw new NotImplementedException();
        }

        public async Task<bool> Update(ViewUpdateInput input)
        {
            //if (!(input?.Id > 0))
            //{
            //    return false;
            //}

            //var entity = await _viewRepository.Get(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    //return ResponseOutput.NotOk("视图不存在！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "视图不存在！");
            //}

            //Mapper.Map(input, entity);
            //await _viewRepository.Update(entity);
            //return true;

            throw new NotImplementedException();
        }

        public async Task<bool> Delete(long id)
        {
            //var result = false;
            //if (id > 0)
            //{
            //    result = await _viewRepository.Delete(m => m.Id == id) > 0;
            //}

            //return result;

            throw new NotImplementedException();
        }

        public async Task<bool> SoftDelete(long id)
        {
            var result = await _viewRepository.SoftDelete(id);

            return result;
        }

        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            var result = await _viewRepository.SoftDelete(ids);

            return result;
        }

       // [Transaction]
        public async Task<bool> Sync(ViewSyncInput input)
        {
            ////查询所有视图
            //var views = await _viewRepository.Select.ToList();
            //var names = views.Select(a => a.Name).ToList();
            //var paths = views.Select(a => a.Path).ToList();

            ////path处理
            //foreach (var view in input.Views)
            //{
            //    view.Path = view.Path?.Trim();
            //}

            ////批量插入
            //{
            //    var inputViews = (from a in input.Views where !(paths.Contains(a.Path) || names.Contains(a.Name)) select a).ToList();
            //    if (inputViews.Count > 0)
            //    {
            //        var insertViews = Mapper.Map<List<ViewEntity>>(inputViews);
            //        foreach (var insertView in insertViews)
            //        {
            //            if (insertView.Label.IsNull())
            //            {
            //                insertView.Label = insertView.Name;
            //            }
            //        }
            //        insertViews = await _viewRepository.Insert(insertViews);
            //        views.AddRange(insertViews);
            //    }
            //}

            ////批量更新
            //{
            //    var inputPaths = input.Views.Select(a => a.Path).ToList();
            //    var inputNames = input.Views.Select(a => a.Name).ToList();

            //    //修改
            //    var updateViews = (from a in views where inputPaths.Contains(a.Path) || inputNames.Contains(a.Name) select a).ToList();
            //    if (updateViews.Count > 0)
            //    {
            //        foreach (var view in updateViews)
            //        {
            //            var inputView = input.Views.Where(a => a.Name == view.Name || a.Path == view.Path).FirstOrDefault();
            //            if (view.Label.IsNull())
            //            {
            //                view.Label = inputView.Label ?? inputView.Name;
            //            }
            //            if (view.Description.IsNull())
            //            {
            //                view.Description = inputView.Description;
            //            }
            //            view.Name = inputView.Name;
            //            view.Path = inputView.Path;
            //            view.Enabled = true;
            //        }
            //    }

            //    //禁用
            //    var disabledViews = (from a in views where (a.Path.NotNull() || a.Name.NotNull()) && (!inputPaths.Contains(a.Path) || !inputNames.Contains(a.Name)) select a).ToList();
            //    if (disabledViews.Count > 0)
            //    {
            //        foreach (var view in disabledViews)
            //        {
            //            view.Enabled = false;
            //        }
            //    }

            //    updateViews.AddRange(disabledViews);
            //    await _viewRepository.UpdateDiy.SetSource(updateViews)
            //    .UpdateColumns(a => new { a.Label, a.Name, a.Path, a.Enabled, a.Description })
            //    .ExecuteAffrows();
            //}


            //return true;

            throw new NotImplementedException();
        }
    }
}