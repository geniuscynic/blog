using Blog.Core.Models;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Blog.Common;
using SqlSugar;
using Blog.Core.ViewModels;
using AutoMapper;
using Blog.IService;

namespace Blog.Service
{
    public class ApiMethodService : BaseServices<ApiMethod>, IApiMethodService
    {
        protected override IBaseRepository<ApiMethod> baseRepository { get; set; }

        public ApiMethodService(IBaseRepository<ApiMethod> repository, IMapper mapper) :base(repository, mapper)
        {
            //this.baseRepository = repository;
        }

    }
}
