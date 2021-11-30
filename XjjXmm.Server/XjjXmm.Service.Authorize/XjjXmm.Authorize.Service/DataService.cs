using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.Authorize.Service.Enums;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Cache;

namespace XjjXmm.Authorize.Service
{
    public class DataService
    {
        private readonly RoleService _roleService;
        private readonly DeptService _deptService;

        public DataService(RoleService roleService, DeptService deptService)
        {
            _roleService = roleService;
            _deptService = deptService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [Caching]
        public async Task<List<long>> GetDeptIds(UserDto user)
        {
            // 用于存储部门id
            var deptIds = new List<long>();
            // 查询用户角色
            var roleSet = await _roleService.FindByUserId(user.Id);
            // 获取对应的部门ID
           

            foreach (var role in roleSet)
            {
                var dataScopeEnum = DataScopeEnum.Find(role.DataScope);
                switch (dataScopeEnum.Key)
                {
                    case DataScopeEnum.THIS_LEVEL:
                        deptIds.Add(user.Dept.Id);
                        break;
                    case DataScopeEnum.CUSTOMIZE:
                        deptIds.AddRange(await GetCustomize(deptIds, role));
                        break;
                    //default:
                      //  return deptIds;
                }
            }
            return deptIds;
        }

       
        /// <summary>
        ///  获取自定义的数据权限
        /// </summary>
        /// <param name="deptIds">部门ID</param>
        /// <param name="role">角色</param>
        /// <returns>数据权限ID</returns> 
        public async Task<List<long>> GetCustomize(List<long> deptIds, RoleSmallDto role)
        {
            var depts = await _deptService.FindByRoleId(role.Id);
            foreach (var dept in depts)
            {
                deptIds.Add(dept.Id);
                var deptChildren = await _deptService.FindByPid(dept.Id);
                if (deptChildren != null && deptChildren.Any())
                {
                    deptIds.AddRange(await _deptService.GetDeptChildren(deptChildren));
                }
            }
            return deptIds;
        }
    }
}
