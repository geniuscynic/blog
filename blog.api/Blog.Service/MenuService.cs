using Blog.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SqlSugar;
using AutoMapper;
using Blog.Common;
using Blog.Entity;
using Blog.Model.Permission;
using Blog.IRepository;

namespace Blog.Service
{
    public class MenuService : BaseServices<Menu>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        //protected override IBaseRepository<Menu> _defaultRepository { get; set; }

        public MenuService(IRepository<Menu> defaultRepository, IMenuRepository menuRepository,  IMapper mapper) :base(defaultRepository, mapper)
        {
            _menuRepository = menuRepository;
            //this._defaultRepository = defaultRepository;
        }


        public async Task<List<Menu>> GetMenus(string token)
        {

            var jwt = JwtHelper.SerializeJwt(token);

            if(jwt.Role.Contains("superAdmin"))
            {
                //return await _defaultRepository.Db.Queryable<Menu>()
                //    .Where(t => t.ParentId == 0)
                //    .Mapper(t => t.ChildMenus, t => t.Id, t => t.Parent.ParentId)
                //    .Mapper(t => t.Buttons, t => t.Buttons.First().MenuId)
                //    .ToListAsync();

                return await _menuRepository.GetMenuForSuperAdmin();


            }


            return await _menuRepository.GetMenuByRole(jwt.Role);
            //_defaultRepository.Db.Queryable<Menu>().ToTree(it => it.Child, it => it.ParentId, 0);
            //return _defaultRepository.Db.Queryable<Menu, MenuPermission, Role>((t, mp, r) => new JoinQueryInfos(
            //                      JoinType.Inner, t.Id == mp.MenuId,
            //                      JoinType.Inner, mp.RoleId == r.Id && jwt.Role.Contains(r.Code) //SqlFunc.ContainsArray(jwt.Role, r.Code)
            //    ))
            //    .Mapper(t => t.Buttons, t => t.Buttons.First().MenuId)
            //    .Mapper((menu, cache) =>
            //    {
            //        var items = cache.Get(ol =>
            //        {
            //            return ol;
            //        });

            //        menu.ChildMenus = items.Where(t => t.ParentId == menu.Id).ToList();

            //    })
            //    .ToListAsync()
            //    .Result
            //    .Where(p => p.ParentId == 0)
            //    .ToList();



            //var query1 = _defaultRepository.Db.Queryable<Menu, MenuPermission, Role>((t, mp, r) => new JoinQueryInfos(
            //                      JoinType.Inner, t.Id == mp.MenuId,
            //                      JoinType.Inner, mp.RoleId == r.Id && jwt.Role.Contains(r.Code) //SqlFunc.ContainsArray(jwt.Role, r.Code)
            //    ));

            ////_defaultRepository.Db.Queryable<Menu>(query1,.Where(t => t.ParentId == 0)
            //var query2 = _defaultRepository.Db.Queryable<Menu>().Where(t => t.ParentId == 0);

            //return await _defaultRepository.Db.Queryable(query1, query2, (p1, p2) =>
            //   p1.Id == p2.Id
            //)
            //.Mapper(p2 => p2.ChildMenus, p2 => p2.Id, p2 => p2.Parent.ParentId)
            //.Select(p2 => p2)
            //    .ToListAsync();

        }

        public async Task<int> AddMenu(AddMenuViewModel addMenuViewModel)
        {
            var menu = _mapper.Map<AddMenuViewModel, Menu>(addMenuViewModel);

            //var length = _defaultRepository.Db.Queryable<Menu>().Where(t => t.ParentId == addMenuViewModel.Pid).Count();
            var length = await _defaultRepository.Count(t => t.ParentId == addMenuViewModel.Pid);
            
            menu.SeqNum = length + 1;

            return await Add(menu);
        }
    }
}
