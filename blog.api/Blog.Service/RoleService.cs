using AutoMapper;
using Blog.Common;
using Blog.Core;
using Blog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IRepository;

namespace Blog.Service
{
    public class RoleService : BaseServices<Role>, IRoleService
    {
        private readonly IRepository<MenuPermission> _menuPermissionRepository;
        private readonly IRepository<ButtonPermission> _buttonPermissionRepository;
        private readonly IRepository<ApiMethodPermission> _apiMethodPremissionRepository;
        private readonly IApiMethodRepository _apiMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IBaseRepository<BlogArticle> blogDefaultRepository;
        //private readonly IMapper _mapper;


        //protected override IBaseRepository<Role> _defaultRepository { get; set; }


        public RoleService(IRepository<Role> userDefaultRepository,
            IRepository<MenuPermission> menuPermissionRepository,
            IRepository<ButtonPermission> buttonPermissionRepository,
            IRepository<ApiMethodPermission> apiMethodPremissionRepository,
            IApiMethodRepository apiMethodRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(userDefaultRepository, mapper)
        {
            _menuPermissionRepository = menuPermissionRepository;
            _buttonPermissionRepository = buttonPermissionRepository;
            _apiMethodPremissionRepository = apiMethodPremissionRepository;
            _apiMethodRepository = apiMethodRepository;
            _unitOfWork = unitOfWork;

            //_defaultRepository = userDefaultRepository;
            //this._mapper = _mapper;
        }



        public async Task<List<MenuPermission>> GetMenusByRole(int roleId)
        {

            return await _menuPermissionRepository.Query(t => t.RoleId == roleId);
        }

        public async Task<List<ButtonPermission>> GetButtonByRole(int roleId)
        {

            return await _buttonPermissionRepository
                .Query(t => t.RoleId == roleId);
        }

        public async Task<List<ApiMethodPermission>> GetApiMethodByRole(int roleId)
        {
            return await _apiMethodPremissionRepository.Query(t => t.RoleId == roleId);
        }

        public async Task<bool> HasApiMethodPermission(List<string> roleCode, string route, string httpMethod)
        {
            return await _apiMethodRepository.HasApiMethodPermission(roleCode, route, httpMethod);
        }


        public async Task<bool> AssignMenuPermission(int roleId, List<int> menus)
        {
            try
            {
                _unitOfWork.BeginTran();
                await _menuPermissionRepository.Delete(t => t.RoleId == roleId);
                //await _defaultRepository.Db.Deleteable<MenuPermission>().Where(t => t.RoleId == roleId).ExecuteCommandAsync();

                var listMenusPermission = new List<MenuPermission>();

                menus.ForEach(t =>
                {
                    listMenusPermission.Add(new MenuPermission()
                    {
                        RoleId = roleId,
                        MenuId = t
                    });
                });



                await _menuPermissionRepository.Add(listMenusPermission);

                //await _defaultRepository.Db.Insertable(listMenusPermission).ExecuteCommandAsync();
                //_defaultRepository.Db.Ado.CommitTran();
                
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        public async Task<bool> AssignButtonPermission(int roleId, List<int> buttons)
        {
            try
            {
                //_defaultRepository.Db.Ado.BeginTran();
                _unitOfWork.BeginTran();

                await _buttonPermissionRepository.Delete(t => t.RoleId == roleId);
                //await _defaultRepository.Db.Deleteable<ButtonPermission>().Where(t => t.RoleId == roleId).ExecuteCommandAsync();

                var listMenusPermission = new List<ButtonPermission>();

                buttons.ForEach(t =>
                {
                    listMenusPermission.Add(new ButtonPermission()
                    {
                        RoleId = roleId,
                        ButtonId = t
                    });
                });

                await _buttonPermissionRepository.Add(listMenusPermission);
                //await _defaultRepository.Db.Insertable(listMenusPermission).ExecuteCommandAsync();
                //_defaultRepository.Db.Ado.CommitTran();
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                //_defaultRepository.Db.Ado.RollbackTran();
                _unitOfWork.Rollback();

                throw;
            }
        }


        public async Task<bool> AssignApiMethodPermission(int roleId, List<int> apis)
        {
            try
            {
                //_defaultRepository.Db.Ado.BeginTran();
                _unitOfWork.BeginTran();
                await _apiMethodPremissionRepository.Delete(t => t.RoleId == roleId);
                //await _defaultRepository.Db.Deleteable<ApiMethodPermission>().Where(t => t.RoleId == roleId).ExecuteCommandAsync();

                var listMenusPermission = new List<ApiMethodPermission>();

                apis.ForEach(t =>
                {
                    listMenusPermission.Add(new ApiMethodPermission()
                    {
                        RoleId = roleId,
                        ApiId = t
                    });
                });

                await _apiMethodPremissionRepository.Add(listMenusPermission);
                //await _defaultRepository.Db.Insertable(listMenusPermission).ExecuteCommandAsync();
                //_defaultRepository.Db.Ado.CommitTran();

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                //_defaultRepository.Db.Ado.RollbackTran();
                throw;
            }
        }
    }
}
