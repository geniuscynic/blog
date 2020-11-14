using AutoMapper;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
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


        public UserService(IBaseRepository<User> blogRepository, IMapper mapper)
        {
            baseRepository = blogRepository;
            this.mapper = mapper;
            
        }


    }
}
