using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Blog.Common;
using SqlSugar;
using AutoMapper;
using Blog.Entity;
using Blog.IService;
using Blog.IRepository;

namespace Blog.Service
{
    public class ApiMethodService : BaseServices<ApiMethod>, IApiMethodService
    {
        
        //protected override IBaseRepository<ApiMethod> _defaultRepository { get; set; }

        public ApiMethodService(IRepository<ApiMethod> defaultRepository, IMapper mapper) :base(defaultRepository, mapper)
        {
           
            //this._defaultRepository = defaultRepository;
        }

    }
}
