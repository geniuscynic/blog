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
        private readonly IHttpContextAccessor _httpContext;

        public RoleController(IRoleService service, IHttpContextAccessor httpContext)
        {
            _service = service;
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
            return new MessageModel<List<ButtonPermission>>
            {
                response = await _service.GetButtonByRole(id)
            };
        }
    }
}
