using AutoMapper;
using Blog.Common;
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

namespace Blog.Service
{
    public class UserService : BaseServices<User>, IUserService
    {
        private readonly IRepository<UserRole> _userRoleRepository;

        private readonly IUnitOfWork _unitOfWork;
        //private readonly IBaseRepository<BlogArticle> blogRepository;
        //private readonly IMapper _mapper;


        //protected override IBaseRepository<User> _repository { get; set; }


        public UserService(IRepository<User> userRepository, IRepository<UserRole> userRoleRepository,
            IMapper mapper, IUnitOfWork unitOfWork) : base(userRepository, mapper)
        {
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
            //_repository = userRepository;
            //this._mapper = _mapper;
        }

        public async Task<User> Add(AddUserViewModel addUserViewModel)
        {
            var user = _mapper.Map<AddUserViewModel, User>(addUserViewModel);

            try
            {
                _unitOfWork.BeginTran();
                var id = await _repository.Add(user);
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
                await _repository.Edit(user);
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
                //await _repository.Db.Deleteable<UserRole>().Where(t => t.UserId == user.Id).ExecuteCommandAsync();
                await _repository.Db.Insertable(userRole).ExecuteCommandAsync();
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
            var user = await _repository.Db.Queryable<User>()
                .Mapper((result, cache) =>
                {
                    var cres = cache.Get(l =>
                    {
                        var r1 = _repository.Db.Queryable<UserRole>()
                            .Mapper(ur => ur.Role, ur => ur.RoleId)
                            .In(it => it.UserId, l.Select(it => it.Id).ToArray()).ToList();
                        //.ToListAsync().Result;

                        return r1;
                    });


                    result.Roles = cres.Where(it => it.UserId == result.Id)
                        .Select(it => it.Role).ToList();
                })
                .ToListAsync();


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
            var user = await _repository.Db.Queryable<User>()
                .Where(t => t.Account == loginViewModel.Login && t.Password == loginViewModel.Password)
                .Mapper((result, cache) =>
                {
                    var cres = cache.Get(l =>
                    {
                        var r1 = _repository.Db.Queryable<UserRole>()
                                  .Mapper(ur => ur.Role, ur => ur.RoleId)
                                  .In(it => it.UserId, l.Select(it => it.Id).ToArray()).ToList();
                        //.ToListAsync().Result;

                        return r1;
                    });


                    result.Roles = cres.Where(it => it.UserId == result.Id)
                                    .Select(it => it.Role).ToList();
                }).FirstAsync();

            if (user == null)
            {
                return "";
            }

            var tokenModel = _mapper.Map<User, TokenModelJwt>(user);
            tokenModel.Role = user.Roles.Select(t => t.Code).ToList();

            var token = JwtHelper.IssueJwt(tokenModel);
            return token;

        }


    }
}
