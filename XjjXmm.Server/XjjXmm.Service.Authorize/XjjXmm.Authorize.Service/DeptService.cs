using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace XjjXmm.Authorize.Service
{
    [Injection]
    public class DeptService
    {
        private readonly DeptRepository _deptRepository;

        public DeptService(DeptRepository deptRepository)
        {
            _deptRepository = deptRepository;
        }

        /// <summary>
        ///  根据 PID 查询
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeptModel>> FindByPid(long pid)
        {
            var dept = await _deptRepository.Query(t => t.Pid == pid);

            return dept.MapTo<DeptEntity, DeptModel>();
        }

        /// <summary>
        ///  根据角色ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeptModel>> FindByRoleId(long id)
        {
            var res = await _deptRepository.FindByRoleId(id);
            return res.MapTo<DeptEntity, DeptModel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptList"></param>
        /// <returns></returns>
        public async Task<List<long>> GetDeptChildren(IEnumerable<DeptModel> deptList)
        {
            var list = new List<long>();
            foreach (var dept in deptList)
            {
                if (dept != null && dept.Enabled)
                {
                    var depts = await FindByPid(dept.Id);
                    if (depts.Any())
                    {
                        list.AddRange(await GetDeptChildren(depts));
                    }
                    list.Add(dept.Id);
                }
            }

            return list;
        }
    }
}
