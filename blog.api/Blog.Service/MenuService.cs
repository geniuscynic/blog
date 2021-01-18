using Blog.IService;
using Blog.Core.Models;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Blog.Common;
using SqlSugar;
using Blog.Core.ViewModels;
using AutoMapper;

namespace Blog.Service
{
    public class MenuService : BaseServices<Menu>, IMenuService
    {
        protected override IBaseRepository<Menu> baseRepository { get; set; }

        public MenuService(IBaseRepository<Menu> repository, IMapper mapper) :base(repository, mapper)
        {
            //this.baseRepository = repository;
        }


        public async Task<List<Menu>> GetMenus(string token)
        {

            var jwt = JwtHelper.SerializeJwt(token);

            if(jwt.Role.Contains("superAdmin"))
            {
                return await baseRepository.Db.Queryable<Menu>()
                    .Where(t => t.ParentId == 0)
                    .Mapper(t => t.ChildMenus, t => t.Id, t => t.Parent.ParentId)
                    .Mapper(t => t.Buttons, t => t.Buttons.First().MenuId)
                    .ToListAsync();
                    
            }
            //baseRepository.Db.Queryable<Menu>().ToTree(it => it.Child, it => it.ParentId, 0);
            return baseRepository.Db.Queryable<Menu, MenuPermission, Role>((t, mp, r) => new JoinQueryInfos(
                                  JoinType.Inner, t.Id == mp.MenuId,
                                  JoinType.Inner, mp.RoleId == r.Id && jwt.Role.Contains(r.Code) //SqlFunc.ContainsArray(jwt.Role, r.Code)
                ))
                .Mapper(t => t.Buttons, t => t.Buttons.First().MenuId)
                .Mapper((menu, cache) =>
                {
                    var items = cache.Get(ol =>
                    {
                        return ol;
                    });

                    menu.ChildMenus = items.Where(t => t.ParentId == menu.Id).ToList();

                })
                .ToListAsync()
                .Result
                .Where(p => p.ParentId == 0)
                .ToList();



            //var query1 = baseRepository.Db.Queryable<Menu, MenuPermission, Role>((t, mp, r) => new JoinQueryInfos(
            //                      JoinType.Inner, t.Id == mp.MenuId,
            //                      JoinType.Inner, mp.RoleId == r.Id && jwt.Role.Contains(r.Code) //SqlFunc.ContainsArray(jwt.Role, r.Code)
            //    ));

            ////baseRepository.Db.Queryable<Menu>(query1,.Where(t => t.ParentId == 0)
            //var query2 = baseRepository.Db.Queryable<Menu>().Where(t => t.ParentId == 0);

            //return await baseRepository.Db.Queryable(query1, query2, (p1, p2) =>
            //   p1.Id == p2.Id
            //)
            //.Mapper(p2 => p2.ChildMenus, p2 => p2.Id, p2 => p2.Parent.ParentId)
            //.Select(p2 => p2)
            //    .ToListAsync();

        }

        public async Task<int> AddMenu(AddMenuViewModel addMenuViewModel)
        {
            var menu = mapper.Map<AddMenuViewModel, Menu>(addMenuViewModel);

            var length = baseRepository.Db.Queryable<Menu>().Where(t => t.ParentId == addMenuViewModel.Pid).Count();
            menu.SeqNum = length + 1;

            return await Add(menu);
        }
    }
}
