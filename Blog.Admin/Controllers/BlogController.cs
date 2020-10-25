using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Blog.Core.IService;
using Blog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.API.Controllers
{
    /// <summary>
    /// blog 的 api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService service;

        public BlogController(IBlogService service)
        {
            this.service = service;
        }

        #region
        //public ObjectResult GetJwtStr()
        //{
        //    string jwtStr = string.Empty;
        //    bool suc = false;

        //    // 获取用户的角色名，请暂时忽略其内部是如何获取的，可以直接用 var userRole="Admin"; 来代替更好理解。
        //    var userRole = "IA, ID";

        //    // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
        //    TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = userRole };
        //    jwtStr = JwtHelper.IssueJwt(tokenModel);//登录，获取到一定规则的 Token 令牌
        //    suc = true;


        //    return Ok(new
        //    {
        //        success = suc,
        //        token = jwtStr
        //    });
        //}

        //[Authorize(Policy = "IA")]
        #endregion

        [HttpGet]
        public IEnumerable<String> Get()
        {
            
            return new string[] { "value1", "value2" };
        }
    

        /// <summary>
        /// 根据id 返回 帖子内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 新增一个 blog
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task<int> Post([FromBody] BlogArticle value)
        {
            return await service.Add(value);


        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
