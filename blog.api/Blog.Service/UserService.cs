using AutoMapper;
using Blog.Common;
using Blog.Core;
using Blog.Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IService;
using Blog.Model.Permission;

namespace Blog.Service
{
    public class UserService : BaseServices<User>, IUserService
    {
        //private readonly IBaseRepository<BlogArticle> blogRepository;
        //private readonly IMapper mapper;


        protected override IBaseRepository<User> baseRepository { get; set; }


        public UserService(IBaseRepository<User> userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            //baseRepository = userRepository;
            //this.mapper = mapper;

        }

        public async Task<User> Add(AddUserViewModel addUserViewModel)
        {
            var user = mapper.Map<AddUserViewModel, User>(addUserViewModel);

            try
            {
                baseRepository.Db.Ado.BeginTran();
                var id = await baseRepository.Add(user);
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


                await baseRepository.Db.Insertable(userRole).ExecuteCommandAsync();
                baseRepository.Db.Ado.CommitTran();

                return user;
            }
            catch (Exception)
            {
                baseRepository.Db.Ado.RollbackTran();
                throw;
            }

        }

        public async Task<User> Edit(AddUserViewModel addUserViewModel)
        {
            var user = mapper.Map<AddUserViewModel, User>(addUserViewModel);

            try
            {
                baseRepository.Db.Ado.BeginTran();
                await baseRepository.Edit(user);
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

                await baseRepository.Db.Deleteable<UserRole>().Where(t => t.UserId == user.Id).ExecuteCommandAsync();
                await baseRepository.Db.Insertable(userRole).ExecuteCommandAsync();
                baseRepository.Db.Ado.CommitTran();

                return user;
            }
            catch (Exception)
            {
                baseRepository.Db.Ado.RollbackTran();
                throw;
            }
        }
        public async Task<List<AddUserViewModel>> GetUsers()
        {
            var user = await baseRepository.Db.Queryable<User>()
                .Mapper((result, cache) =>
                {
                    var cres = cache.Get(l =>
                    {
                        var r1 = baseRepository.Db.Queryable<UserRole>()
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
            //var addUserModel = mapper.Map<List<User>, List<AddUserViewModel>>(user);
            //addUserModel.Ro

        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            //mapper.Map<LoginViewModel, User>(blogViewModel);
            //throw new NotImplementedException();
            var user = await baseRepository.Db.Queryable<User>()
                .Where(t => t.Account == loginViewModel.Login && t.Password == loginViewModel.Password)
                .Mapper((result, cache) =>
                {
                    var cres = cache.Get(l =>
                    {
                        var r1 = baseRepository.Db.Queryable<UserRole>()
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

            var tokenModel = mapper.Map<User, TokenModelJwt>(user);
            tokenModel.Role = user.Roles.Select(t => t.Code).ToList();

            var token = JwtHelper.IssueJwt(tokenModel);
            return token;

        }


    }
}
