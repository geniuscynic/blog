using Blog.Core;
using Blog.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.Model.Permission;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "superAdmin")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService service;
        private readonly IHttpContextAccessor httpContext;

        public MenuController(IMenuService service, IHttpContextAccessor httpContext)
        {
            this.service = service;
            this.httpContext = httpContext;
        }

        // GET: api/<MenuController>
        [HttpGet]
        public async Task<MessageModel<IEnumerable<Menu>>> Get()
        {
            var token = httpContext.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return new MessageModel<IEnumerable<Menu>>
            {
                response = await service.GetMenus(token)
            };
        }

        // GET api/<MenuController>/5
        [HttpPut]
        public async Task<MessageModel<bool>> Put([FromBody] Menu menu)
        {
            return new MessageModel<bool>
            {
                response = await service.Edit(menu)
            };
        }

        // POST api/<MenuController>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromBody] AddMenuViewModel value)
        {
            return new MessageModel<int>
            {
                response = await service.AddMenu(value)
            };
        }

        // PUT api/<MenuController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<MenuController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
