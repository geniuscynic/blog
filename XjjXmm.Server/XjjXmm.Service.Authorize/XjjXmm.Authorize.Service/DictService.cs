﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Cache;

namespace XjjXmm.Authorize.Service
{
    public class DictService
    {
        private readonly DictRepository _dictRepository;
        private readonly ICache _cache;


        public DictService(DictRepository dictRepository, ICache cache)
        {
            _dictRepository = dictRepository;
            _cache = cache;
        }

        //public Map<String, Object> queryAll(DictQueryCriteria dict, Pageable pageable)
        //{
        //    Page<Dict> page = dictRepository.findAll((root, query, cb)->QueryHelp.getPredicate(root, dict, cb), pageable);
        //    return PageUtil.toPage(page.map(dictMapper::toDto));
        //}


        //public List<DictDto> queryAll(DictQueryCriteria dict)
        //{
        //    List<Dict> list = dictRepository.findAll((root, query, cb)->QueryHelp.getPredicate(root, dict, cb));
        //    return dictMapper.toDto(list);
        //}


        //  @Transactional(rollbackFor = Exception.class)
        //public void create(Dict resources)
        //{
        //    dictRepository.save(resources);
        //}


        //   @Transactional(rollbackFor = Exception.class)
        //        public void update(Dict resources)
        //        {
        //            // 清理缓存
        //            delCaches(resources);
        //            Dict dict = dictRepository.findById(resources.getId()).orElseGet(Dict::new);
        //            ValidationUtil.isNull(dict.getId(), "Dict", "id", resources.getId());
        //            dict.setName(resources.getName());
        //            dict.setDescription(resources.getDescription());
        //            dictRepository.save(dict);
        //        }


        //        //    @Transactional(rollbackFor = Exception.class)
        //        public void delete(Set<Long> ids)
        //        {
        //            // 清理缓存
        //            List<Dict> dicts = dictRepository.findByIdIn(ids);
        //            for (Dict dict : dicts)
        //            {
        //                delCaches(dict);
        //            }
        //            dictRepository.deleteByIdIn(ids);
        //        }


        //        public void download(List<DictDto> dictDtos, HttpServletResponse response) throws IOException
        //        {
        //            List<Map<String, Object>> list = new ArrayList<>();
        //        for (DictDto dictDTO : dictDtos) {
        //            if(CollectionUtil.isNotEmpty(dictDTO.getDictDetails())){
        //                for (DictDetailDto dictDetail : dictDTO.getDictDetails()) {
        //                    Map<String, Object> map = new LinkedHashMap<>();
        //        map.put("字典名称", dictDTO.getName());
        //                    map.put("字典描述", dictDTO.getDescription());
        //                    map.put("字典标签", dictDetail.getLabel());
        //                    map.put("字典值", dictDetail.getValue());
        //                    map.put("创建日期", dictDetail.getCreateTime());
        //                    list.add(map);
        //                }
        //} else
        //{
        //    Map<String, Object> map = new LinkedHashMap<>();
        //    map.put("字典名称", dictDTO.getName());
        //    map.put("字典描述", dictDTO.getDescription());
        //    map.put("字典标签", null);
        //    map.put("字典值", null);
        //    map.put("创建日期", dictDTO.getCreateTime());
        //    list.add(map);
        //}
        //        }
        //        FileUtil.downloadExcel(list, response);
        //    }

        //    public void delCaches(Dict dict)
        //{
        //    redisUtils.del(CacheKey.DICT_NAME + dict.getName());
        //}
    }
}
