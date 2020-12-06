using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async  Task<MessageModel<IEnumerable<AddUserViewModel>>> Get()
        {
            return new MessageModel<IEnumerable<AddUserViewModel>>
            {
                response = await _userService.GetUsers()
            };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<MessageModel<User>> Post([FromBody] AddUserViewModel user)
        {
            return new MessageModel<User>()
            {
               response = await  _userService.Add(user)
            };
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<MessageModel<User>> Put([FromBody] AddUserViewModel user)
        {
            return new MessageModel<User>
            {
                response = await _userService.Edit(user)
            };
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
