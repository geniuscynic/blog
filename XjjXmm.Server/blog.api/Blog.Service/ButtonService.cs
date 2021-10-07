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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using XjjXmm.Framework.Jwt;

namespace Blog.Service
{
    public class ButtonService : BaseServices<Button>, IButtonService
    {
        private readonly IButtonRepository _buttonRepository;
        //protected override IBaseRepository<Button> _defaultRepository { get; set; }

        public ButtonService(IRepository<Button> defaultRepository, IButtonRepository buttonRepository,  IMapper mapper) :base(defaultRepository, mapper)
        {
            _buttonRepository = buttonRepository;
            //this._defaultRepository = defaultRepository;
        }

        public async Task<List<Button>> GetButtons(string token)
        {
            var jwt = JwtHelper.SerializeJwt(token);

            if (jwt.Role.Contains("superAdmin"))
            {
                //return await _defaultRepository.Db.Queryable<Button>()
                //    .ToListAsync();

                return await _buttonRepository.GetAll();

            }
            //_defaultRepository.Db.Queryable<Menu>().ToTree(it => it.Child, it => it.ParentId, 0);
            return await _buttonRepository.GetButtons(token); 
                
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
