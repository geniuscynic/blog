using Blog.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SqlSugar;
using AutoMapper;
using Blog.Entity;
using Blog.Model.Permission;
using Blog.IRepository;
using XjjXmm.Framework.Jwt;

namespace Blog.Service
{
    public class PermissionService : BaseServices<Button>, IPermissionService
    {
        private readonly IButtonRepository _buttonRepository;
        //protected override IBaseRepository<Button> _defaultRepository { get; set; }

        public PermissionService(IRepository<Button> defaultRepository,
            IButtonRepository buttonRepository,
            
            IMapper mapper) :base(defaultRepository, mapper)
        {
            _buttonRepository = buttonRepository;
            //this._defaultRepository = defaultRepository;
        }


       

        public async Task<List<Button>> GetButtons(string token)
        {
            var jwt = JwtHelper.SerializeJwt(token);

            if (jwt.Role.Contains("superAdmin"))
            {
                return await _defaultRepository.GetAll();

            }
            //_defaultRepository.Db.Queryable<Menu>().ToTree(it => it.Child, it => it.ParentId, 0);
            //return await _defaultRepository.Db.Queryable<Button, ButtonPermission, Role>((t, mp, r) => new JoinQueryInfos(
            //                      JoinType.Inner, t.Id == mp.ButtonId,
            //                      JoinType.Inner, mp.RoleId == r.Id  //SqlFunc.ContainsArray(jwt.Role, r.Code)
            //    ))
            //    .ToListAsync(); 

            return await _buttonRepository.GetButtonsByRole(jwt.Role);


        }

        public async Task<int> AddButton(AddButtonViewModel addButtonViewModel)
        {
            var button = _mapper.Map<AddButtonViewModel, Button>(addButtonViewModel);

            //var length = _defaultRepository.Db.Queryable<Menu>().Where(t => t.ParentId == addMenuViewModel.Pid).Count();
            //menu.SeqNum = length + 1;

            return await Add(button);
        }
    }
}
