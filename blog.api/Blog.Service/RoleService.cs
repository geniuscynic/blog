using AutoMapper;
using Blog.Common;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Core.ViewModels;
using Blog.Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class RoleService : BaseServices<Role>, IRoleService
    {
        //private readonly IBaseRepository<BlogArticle> blogRepository;
        //private readonly IMapper mapper;


        protected override IBaseRepository<Role> baseRepository { get; set; }


        public RoleService(IBaseRepository<Role> userRepository, IMapper mapper):base(userRepository, mapper)
        {
            //baseRepository = userRepository;
            //this.mapper = mapper;

        }

        public async Task<List<MenuPermission>> GetMenusByRole(int roleId)
        {

            return await baseRepository.Db.Queryable<MenuPermission>()
                .Where(t => t.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<List<ButtonPermission>> GetButtonByRole(int roleId)
        {

            return await baseRepository.Db.Queryable<ButtonPermission>()
                .Where(t => t.RoleId == roleId)
                .ToListAsync();
        }


        public Task<bool> AssignMenuPermission(int roleId, List<Menu> menus)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AssignButtonPermission(int roleId, List<Button> buttons)
        {
            throw new NotImplementedException();
        }
    }
}
