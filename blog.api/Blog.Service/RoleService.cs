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


        public async Task<bool> AssignMenuPermission(int roleId, List<int> menus)
        {
            try
            {
                baseRepository.Db.Ado.BeginTran();
                await baseRepository.Db.Deleteable<MenuPermission>().Where(t => t.RoleId == roleId).ExecuteCommandAsync();

                var listMenusPermission = new List<MenuPermission>();

                menus.ForEach(t =>
                {
                    listMenusPermission.Add(new MenuPermission()
                    {
                        RoleId = roleId,
                        MenuId = t
                    });
                });


                


                await baseRepository.Db.Insertable(listMenusPermission).ExecuteCommandAsync();
                baseRepository.Db.Ado.CommitTran();

                return true;
            }
            catch (Exception)
            {
                baseRepository.Db.Ado.RollbackTran();
                throw;
            }
        }

        public async Task<bool> AssignButtonPermission(int roleId, List<int> buttons)
        {
            try
            {
                baseRepository.Db.Ado.BeginTran();
                await baseRepository.Db.Deleteable<ButtonPermission>().Where(t => t.RoleId == roleId).ExecuteCommandAsync();

                var listMenusPermission = new List<ButtonPermission>();

                buttons.ForEach(t =>
                {
                    listMenusPermission.Add(new ButtonPermission()
                    {
                        RoleId = roleId,
                        ButtonId = t
                    });
                });

                await baseRepository.Db.Insertable(listMenusPermission).ExecuteCommandAsync();
                baseRepository.Db.Ado.CommitTran();

                return true;
            }
            catch (Exception)
            {
                baseRepository.Db.Ado.RollbackTran();
                throw;
            }
        }
    }
}
