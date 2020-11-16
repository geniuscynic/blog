using AutoMapper;
using Blog.Common;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Core.VeiwModels;
using Blog.Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class UserService : BaseServices<User>, IUserService
    {
        //private readonly IBaseRepository<BlogArticle> blogRepository;
        private readonly IMapper mapper;


        protected override IBaseRepository<User> baseRepository { get; set; }


        public UserService(IBaseRepository<User> userRepository, IMapper mapper)    :base(userRepository)
        {
            //baseRepository = userRepository;
            this.mapper = mapper;

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

            if(user == null)
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
