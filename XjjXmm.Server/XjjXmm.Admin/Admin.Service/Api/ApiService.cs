using Admin.Repository.Api;
using Admin.Repository.Api.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.Api
{
    [Injection]
    public class ApiService : BaseService, IApiService
    {
        private readonly IApiRepository _apiRepository;

        public ApiService(IApiRepository moduleRepository)
        {
            _apiRepository = moduleRepository;
        }

        public async Task<ApiGetOutput> GetAsync(long id)
        {

            var result = await _apiRepository.GetById(id);
            return result.MapTo<ApiEntity, ApiGetOutput>();
          
        }

        public async Task<List<ApiListOutput>> ListAsync(string key)
        {
            
            var result = await _apiRepository.Query(!string.IsNullOrEmpty(key), a => a.Path.Contains(key) || a.Label.Contains(key));
            
            //var data = await _apiRepository
            //    .WhereIf(key.NotNull(), a => a.Path.Contains(key) || a.Label.Contains(key))
            //    .ToListAsync<ApiListOutput>();

            //return data;
            return result.MapTo<ApiEntity, ApiListOutput>().ToList();
        }

        public async Task<PageOutput<ApiEntity>> PageAsync(PageInput<ApiEntity> input)
        {
            //var key = input.Filter?.Label;

            //var list = await _apiRepository.Select
            //.WhereIf(key.NotNull(), a => a.Path.Contains(key) || a.Label.Contains(key))
            //.Count(out var total)
            //.OrderByDescending(true, c => c.Id)
            //.Page(input.CurrentPage, input.PageSize)
            //.ToListAsync();

            //var data = new PageOutput<ApiEntity>()
            //{
            //    List = list,
            //    Total = total
            //};

            //return data;

           return await _apiRepository.Page(input);
        }

        public async Task<bool> AddAsync(ApiAddInput input)
        {
            //var entity = Mapper.Map<ApiEntity>(input);
            //var id = (await _apiRepository.InsertAsync(entity)).Id;

            //return id > 0;
                                                          
            var entity = input.MapTo<ApiAddInput, ApiEntity>();
            return await _apiRepository.Add(entity) > 0;
        }

        public async Task<bool> UpdateAsync(ApiUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            //var entity = await _apiRepository.GetAsync(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    //return ResponseOutput.NotOk("接口不存在！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "接口不存在！");
            //}

            //Mapper.Map(input, entity);
            var entity = input.MapTo<ApiUpdateInput, ApiEntity>();

            return await _apiRepository.Update(entity);
            //return true;

            throw new System.Exception();
        }

        public async Task<bool> Delete(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = await _apiRepository.Delete(id);
            }

            return result;

           
        }

        public async Task<bool> SoftDelete(long id)
        {
            var result = await _apiRepository.SoftDelete(id);
            return result;


        }

        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            var result = await _apiRepository.SoftDelete(ids);

            return result;

          
        }

        //[Transaction]
        public async Task<bool> SyncAsync(ApiSyncInput input)
        {
            /* //查询所有api
             var apis = await _apiRepository.Select.ToListAsync();
             var paths = apis.Select(a => a.Path).ToList();

             //path处理
             foreach (var api in input.Apis)
             {
                 api.Path = api.Path?.Trim().ToLower();
                 api.ParentPath = api.ParentPath?.Trim().ToLower();
             }

             #region 执行插入

             //执行父级api插入
             var parentApis = input.Apis.FindAll(a => a.ParentPath.IsNull());
             var pApis = (from a in parentApis where !paths.Contains(a.Path) select a).ToList();
             if (pApis.Count > 0)
             {
                 var insertPApis = Mapper.Map<List<ApiEntity>>(pApis);
                 insertPApis = await _apiRepository.InsertAsync(insertPApis);
                 apis.AddRange(insertPApis);
             }

             //执行子级api插入
             var childApis = input.Apis.FindAll(a => a.ParentPath.NotNull());
             var cApis = (from a in childApis where !paths.Contains(a.Path) select a).ToList();
             if (cApis.Count > 0)
             {
                 var insertCApis = Mapper.Map<List<ApiEntity>>(cApis);
                 insertCApis = await _apiRepository.InsertAsync(insertCApis);
                 apis.AddRange(insertCApis);
             }

             #endregion 执行插入

             #region 修改和禁用

             {
                 //api修改
                 ApiEntity a;
                 List<string> labels;
                 string label;
                 string desc;
                 foreach (var api in parentApis)
                 {
                     a = apis.Find(a => a.Path == api.Path);
                     if (a?.Id > 0)
                     {
                         labels = api.Label?.Split("\r\n")?.ToList();
                         label = labels != null && labels.Count > 0 ? labels[0] : string.Empty;
                         desc = labels != null && labels.Count > 1 ? string.Join("\r\n", labels.GetRange(1, labels.Count() - 1)) : string.Empty;
                         a.ParentId = 0;
                         a.Label = label;
                         a.Description = desc;
                         a.Enabled = true;
                     }
                 }
             }

             {
                 //api修改
                 ApiEntity a;
                 ApiEntity pa;
                 List<string> labels;
                 string label;
                 string desc;
                 foreach (var api in childApis)
                 {
                     a = apis.Find(a => a.Path == api.Path);
                     pa = apis.Find(a => a.Path == api.ParentPath);
                     if (a?.Id > 0)
                     {
                         labels = api.Label?.Split("\r\n")?.ToList();
                         label = labels != null && labels.Count > 0 ? labels[0] : string.Empty;
                         desc = labels != null && labels.Count > 1 ? string.Join("\r\n", labels.GetRange(1, labels.Count() - 1)) : string.Empty;

                         a.ParentId = pa.Id;
                         a.Label = label;
                         a.Description = desc;
                         a.HttpMethods = api.HttpMethods;
                         a.Enabled = true;
                     }
                 }
             }

             {
                 //api禁用
                 var inputPaths = input.Apis.Select(a => a.Path).ToList();
                 var disabledApis = (from a in apis where !inputPaths.Contains(a.Path) select a).ToList();
                 if (disabledApis.Count > 0)
                 {
                     foreach (var api in disabledApis)
                     {
                         api.Enabled = false;
                     }
                 }
             }

             #endregion 修改和禁用

             //批量更新
             await _apiRepository.UpdateDiy.SetSource(apis)
             .UpdateColumns(a => new { a.ParentId, a.Label, a.HttpMethods, a.Description, a.Enabled })
             .ExecuteAffrowsAsync();

             return true;*/

            throw new System.Exception();
        }
    }
}