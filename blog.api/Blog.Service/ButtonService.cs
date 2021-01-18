using Blog.IService;
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

namespace Blog.Service
{
    public class ButtonService : BaseServices<Button>, IButtonService
    {
        protected override IBaseRepository<Button> baseRepository { get; set; }

        public ButtonService(IBaseRepository<Button> repository, IMapper mapper) :base(repository, mapper)
        {
            //this.baseRepository = repository;
        }

        public async Task<List<Button>> GetButtons(string token)
        {
            var jwt = JwtHelper.SerializeJwt(token);

            if (jwt.Role.Contains("superAdmin"))
            {
                return await baseRepository.Db.Queryable<Button>()
                    .ToListAsync();

            }
            //baseRepository.Db.Queryable<Menu>().ToTree(it => it.Child, it => it.ParentId, 0);
            return await baseRepository.Db.Queryable<Button, ButtonPermission, Role>((t, mp, r) => new JoinQueryInfos(
                                  JoinType.Inner, t.Id == mp.ButtonId,
                                  JoinType.Inner, mp.RoleId == r.Id  //SqlFunc.ContainsArray(jwt.Role, r.Code)
                ))
                .ToListAsync(); 
                
        }

        public async Task<int> AddButton(AddButtonViewModel addButtonViewModel)
        {
            var button = mapper.Map<AddButtonViewModel, Button>(addButtonViewModel);

            //var length = baseRepository.Db.Queryable<Menu>().Where(t => t.ParentId == addMenuViewModel.Pid).Count();
            //menu.SeqNum = length + 1;

            return await Add(button);
        }
    }
}
