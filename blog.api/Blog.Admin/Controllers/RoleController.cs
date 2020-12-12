using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
using log4net.Appender;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        private readonly IApiMethodService _apiMethodService;
        private readonly IHttpContextAccessor _httpContext;

        public RoleController(IRoleService service, IApiMethodService apiMethodService, IHttpContextAccessor httpContext)
        {
            _service = service;
            _apiMethodService = apiMethodService;
            _httpContext = httpContext;
        }


        // GET: api/<RoleController>
        [HttpGet]
        public async Task<MessageModel<IEnumerable<Role>>> Get()
        {
            return new MessageModel<IEnumerable<Role>>
            {
                response = await _service.GetAll()
            };
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromBody] Role role)
        {
            return new MessageModel<int>
            {
                response = await _service.Add(role)
            };
        }

        // PUT api/<RoleController>/5
        [HttpPut]
        public async Task<MessageModel<bool>> Put([FromBody] Role role)
        {
            return new MessageModel<bool>
            {
                response = await _service.Edit(role)
            };
        }
    

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("GetApiMethods")]
        public async Task<MessageModel<List<ApiMethod>>> GetApiMethods()
        {
            return new MessageModel<List<ApiMethod >>
            {
                response = await _apiMethodService.GetAll()
            };
        }

        [HttpGet("{id}/menu")]
        public async Task<MessageModel<List<MenuPermission>>> GetMenusByRole(int id)
        {
            return new MessageModel<List<MenuPermission>>
            {
                response = await _service.GetMenusByRole(id)
            };
        }

        [HttpGet("{id}/button")]
        public async Task<MessageModel<List<ButtonPermission>>> GetButtonsByRole(int id)
        {
            //var a = await _service.GetButtonByRole(id);
            return new MessageModel<List<ButtonPermission>>
            {
                response = await _service.GetButtonByRole(id)
            };
        }

        [HttpGet("{id}/apis")]
        public async Task<MessageModel<List<ApiMethodPermission>>> GetApiMethodByRole(int id)
        {
            //var a = await _service.GetApiMethodByRole(id);
            return new MessageModel<List<ApiMethodPermission>>
            {
                response = await _service.GetApiMethodByRole(id)
            };
        }

        [HttpPost("{id}/menu")]
        public async Task<MessageModel<bool>> AssignMenuPermission(int id, [FromBody] List<int> menus)
        {
            return new MessageModel<bool>
            {
                response = await _service.AssignMenuPermission(id, menus)
            };
        }


        [HttpPost("{id}/button")]
        public async Task<MessageModel<bool>> AssignButtonPermission(int id, [FromBody] List<int> buttons)
        {
            return new MessageModel<bool>
            {
                response = await _service.AssignButtonPermission(id, buttons)
            };
        }

        [HttpPost("{id}/apis")]
        public async Task<MessageModel<bool>> AssignApisPermission(int id, [FromBody] List<int> apis)
        {
            return new MessageModel<bool>
            {
                response = await _service.AssignApiMethodPermission(id, apis)
            };
        }
    }
}
