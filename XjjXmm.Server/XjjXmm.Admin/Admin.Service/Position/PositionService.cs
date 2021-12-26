using Admin.Repository.Position;
using System;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.Position
{
    [Injection]
    public class PositionService : BaseService, IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(
            IPositionRepository positionRepository
        )
        {
            _positionRepository = positionRepository;
        }

        public async Task<PositionGetOutput> Get(long id)
        {
            var result = await _positionRepository.GetById(id);


            return result.MapTo<PositionEntity, PositionGetOutput>();
        }

        public async Task<PageOutput<PositionListOutput>> Page(PageInput<PositionListInput> input)
        {
            var parm = input.MapTo<PageInput<PositionListInput>, PageInput<PositionEntity>>();

            var res = await _positionRepository.QueryPage(parm);
            //return res.MapTo<PageOutput<PositionEntity>, PageOutput<PositionListOutput>>();

            return new PageOutput<PositionListOutput>
            {
                CurrentPage = res.CurrentPage,
                Total = res.Total,
                PageSize = res.PageSize,
                Data = res.Data.MapTo<PositionEntity, PositionListOutput>()
            };
        }

        public async Task<bool> Add(PositionAddInput input)
        {
            var entity = input.MapTo<PositionAddInput, PositionEntity>();
            Fill(entity, FillStatus.Add);

            return await _positionRepository.Add(entity) > 0;


        }

        public async Task<bool> Update(PositionUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            //var entity = await _positionRepository.GetAsync(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    return ResponseOutput.NotOk("职位不存在！");
            //}

            //Mapper.Map(input, entity);

            var entity = input.MapTo<PositionUpdateInput, PositionEntity>();
            Fill(entity, FillStatus.Update);

            return await _positionRepository.Update(entity);

        }

        public async Task<bool> Delete(long id)
        {

            if (id > 0)
            {
                return await _positionRepository.Delete(id);
            }

            return false;
        }

        public async Task<bool> SoftDelete(long id)
        {
            return await _positionRepository.SoftDelete(id);


        }

        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _positionRepository.SoftDelete(ids);
        }
    }
}