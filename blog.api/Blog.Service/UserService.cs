using AutoMapper;
using Blog.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IRepository;
using Blog.IService;
using Blog.Model.Permission;
using XjjXmm.Framework.Jwt;

namespace Blog.Service
{
    public class UserService : BaseServices<User>, IUserService
    {
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;
        //private readonly IBaseRepository<BlogArticle> blogDefaultRepository;
        //private readonly IMapper _mapper;


        //protected override IBaseRepository<User> _defaultRepository { get; set; }


        public UserService(IRepository<User> userDefaultRepository, IRepository<UserRole> userRoleRepository, 
            IUserRepository userRepository,
            IMapper mapper, IUnitOfWork unitOfWork) : base(userDefaultRepository, mapper)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            //_defaultRepository = userDefaultRepository;
            //this._mapper = _mapper;
        }

        public async Task<User> Add(AddUserViewModel addUserViewModel)
        {
            var user = _mapper.Map<AddUserViewModel, User>(addUserViewModel);

            try
            {
                _unitOfWork.BeginTran();
                var id = await _defaultRepository.Add(user);
                user.Id = id;

                var userRole = new List<UserRole>();
                addUserViewModel.Roles?.ForEach(t =>
                {
                    userRole.Add(new UserRole
                    {
                        RoleId = t,
                        UserId = id

                    });
                });


                await _userRoleRepository.Add(userRole);
                _unitOfWork.Commit();

                return user;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task<User> Edit(AddUserViewModel addUserViewModel)
        {
            var user = _mapper.Map<AddUserViewModel, User>(addUserViewModel);

            try
            {
                _unitOfWork.BeginTran();
                await _defaultRepository.Edit(user);
                //user.Id = id;

                var userRole = new List<UserRole>();
                addUserViewModel.Roles?.ForEach(t =>
                {
                    userRole.Add(new UserRole
                    {
                        RoleId = t,
                        UserId = user.Id

                    });
                });

                await _userRoleRepository.Delete(t => t.UserId == user.Id);
                await _userRoleRepository.Add(userRole);
                //await _defaultRepository.Db.Deleteable<UserRole>().Where(t => t.UserId == user.Id).ExecuteCommandAsync();
                //await _defaultRepository.Db.Insertable(userRole).ExecuteCommandAsync();
                
                _unitOfWork.Commit();

                return user;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<List<AddUserViewModel>> GetUsers()
        {
            var user = await _userRepository.GetUser();


            var result = user.Select(t => new AddUserViewModel
            {
                Id = t.Id,
                Account = t.Account,
                NickName = t.NickName,
                Password = t.Password,
                Roles = t.Roles.Select(tr => tr.Id).ToList()
            }).ToList();

            return result;
            //var addUserModel = _mapper.Map<List<User>, List<AddUserViewModel>>(user);
            //addUserModel.Ro

        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            //_mapper.Map<LoginViewModel, User>(blogViewModel);
            //throw new NotImplementedException();
            var users = await _userRepository.GetUser(t => t.Account == loginViewModel.Login && t.Password == loginViewModel.Password);

            var user = users.FirstOrDefault();
                
                

            if (user == null)
            {
                return "";
            }

            //todo
            var tokenModel = _mapper.Map<User, TokenModelJwt>(user);
            tokenModel.Role = user.Roles.Select(t => t.Code).ToList();

            //var token = JwtHelper.IssueJwt(tokenModel);
            return "";

        }


    }
}
