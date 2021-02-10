using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IRepository;
using SqlSugar;

namespace Blog.Repository
{
    public class MenuRepository : Repository<Menu> , IMenuRepository
    {
        public MenuRepository(Dbcontext dbcontext) : base(dbcontext)
        {
        }


        public async Task<List<Menu>> GetMenuForSuperAdmin()
        {
            return await Db.Queryable<Menu>()
                .Where(t => t.ParentId == 0)
                .Mapper(t => t.ChildMenus, t => t.Id, t => t.Parent.ParentId)
                .Mapper(t => t.Buttons, t => t.Buttons.First().MenuId)
                .ToListAsync();
        }

        public async Task<List<Menu>> GetMenuByRole(List<string> roles)
        {
            var result =  await Db.Queryable<Menu, MenuPermission, Role>((t, mp, r) => new JoinQueryInfos(
                    JoinType.Inner, t.Id == mp.MenuId,
                    JoinType.Inner, mp.RoleId == r.Id && roles.Contains(r.Code) //SqlFunc.ContainsArray(jwt.Role, r.Code)
                ))
                .Mapper(t => t.Buttons, t => t.Buttons.First().MenuId)
                .Mapper((menu, cache) =>
                {
                    var items = cache.Get(ol => ol);

                    menu.ChildMenus = items.Where(t => t.ParentId == menu.Id).ToList();

                })
                .ToListAsync();
                
                return result
                .Where(p => p.ParentId == 0)
                .ToList();
        }
    }
}
