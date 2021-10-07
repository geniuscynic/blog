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
    public class ButtonController : ControllerBase
    {
        private readonly IButtonService _service;
        private readonly IHttpContextAccessor _httpContext;

        public ButtonController(IButtonService service, IHttpContextAccessor httpContext)
        {
            this._service = service;
            this._httpContext = httpContext;
        }

        // GET: api/<MenuController>
        [HttpGet]
        public async Task<MessageModel<IEnumerable<Button>>> Get()
        {
            var token = _httpContext.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return new MessageModel<IEnumerable<Button>>
            {
                response = await _service.GetButtons(token)
            };
        }

        // GET api/<MenuController>/5
        [HttpPut]
        public async Task<MessageModel<bool>> Put([FromBody] Button button)
        {
            return new MessageModel<bool>
            {
                response = await _service.Edit(button)
            };
        }

        // POST api/<MenuController>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromBody] AddButtonViewModel value)
        {
            return new MessageModel<int>
            {
                response = await _service.AddButton(value)
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
