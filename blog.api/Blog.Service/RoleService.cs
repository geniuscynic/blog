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
using System.Runtime.InteropServices.ComTypes;
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

        public async Task<List<ApiMethodPermission>> GetApiMethodByRole(int roleId)
        {
            return await baseRepository.Db.Queryable<ApiMethodPermission>()
                .Where(t => t.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<bool> HasApiMethodPermission(List<string> roleCode, string route, string httpMethod)
        {
            return await baseRepository.Db.Queryable<ApiMethod, ApiMethodPermission, Role>(
                    (t1, t2, t3) => t1.Id == t2.ApiId && t2.RoleId == t3.Id)
                .Where((t1, t2, t3) => roleCode.Contains(t3.Code))
                .Where(t1 => t1.RoutePath == route && t1.HttpMethod == httpMethod)
                .Select(t1 => t1)
                .AnyAsync();
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


        public async Task<bool> AssignApiMethodPermission(int roleId, List<int> apis)
        {
            try
            {
                baseRepository.Db.Ado.BeginTran();
                await baseRepository.Db.Deleteable<ApiMethodPermission>().Where(t => t.RoleId == roleId).ExecuteCommandAsync();

                var listMenusPermission = new List<ApiMethodPermission>();

                apis.ForEach(t =>
                {
                    listMenusPermission.Add(new ApiMethodPermission()
                    {
                        RoleId = roleId,
                        ApiId = t
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
