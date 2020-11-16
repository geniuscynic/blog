using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Blog.Service
{
    public class MenuService : BaseServices<Menu>, IMenuService
    {
        protected override IBaseRepository<Menu> baseRepository { get; set; }

        public MenuService(IBaseRepository<Menu> repository):base(repository)
        {
            //this.baseRepository = repository;
        }


        public async   Task<List<Menu>> Get()
        {
            return await baseRepository.Db.Queryable<Menu>()
                .Where(t => t.ParentId == 0)
                .Mapper(it => it.ChildMenus, it=>it.Id, it => it.ChildMenus.First().ParentId)
                .ToListAsync();

        }

         
    }
}
