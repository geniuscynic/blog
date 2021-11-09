using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.DataBase;
using XjjXmm.DataBase.Imp.Operate;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Repository
{
    [Injection]
    public class MenuRepository: Repository<MenuEntity>
    {
        public MenuRepository(DbClient dbclient) : base(dbclient)
        {
        }

        public async Task<IEnumerable<MenuEntity>> FindByRoleIdsAndTypeNot(List<long> roleIds, int type)
        {
            //var roles = roleIds.Select(t => t.ToString());

            return await _dbclient.ComplexQueryable<MenuEntity>("m")
                .Join<RoleMenuEntity>("rm", (m, rm) => m.Id == rm.MenuId)
                //.Where((m, rm) => m.Type != type)
                .Where((m, rm) => SqlFunc.Contain(roleIds, rm.RoleId))
               // .OrderBy((m, rm) => m.MenuSort)
                .ExecuteQuery();
        }

        public async Task<IEnumerable<MenuEntity>> FindByUserIdsAndTypeNot(long userId, int type)
        {
            //var roles = roleIds.Select(t => t.ToString());

            return await _dbclient.ComplexQueryable<MenuEntity>("m")
                .Join<RoleMenuEntity>("rm", (m, rm) => m.Id == rm.MenuId)
               // .Join<RoleEntity>("re", (m,rm,re)=> rm.RoleId==re.Id)
                .Join<UserRoleEntity>("ue", (m,  rm, ue) => ue.UserId == userId && ue.RoleId == rm.RoleId)
                .Where((m, rm) => m.Type != type)
                //.Where((m, rm) => SqlFunc.Contain(roleIds, rm.RoleId))
                 .OrderBy((m, rm) => m.MenuSort)
                .ExecuteQuery();
        }

        /**
    * 用户角色改变时需清理缓存
    * @param currentUserId /
    * @return /
    */
        //@Override
        // @Cacheable(key = "'user:' + #p0")
        // public List<MenuDto> findByUser(long currentUserId)
        // {
        //List<RoleSmallDto> roles = roleService.findByUsersId(currentUserId);
        //Set<Long> roleIds = roles.stream().map(RoleSmallDto::getId).collect(Collectors.toSet());
        //LinkedHashSet<Menu> menus = menuRepository.findByRoleIdsAndTypeNot(roleIds, 2);
        //return menus.stream().map(menuMapper::toDto).collect(Collectors.toList());
        // }
    }
}
