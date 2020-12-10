using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Core.ViewModels;
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

        /// <summary>
        /// 获取 blog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "mypermission")]
        [Authorize]
        public async Task<MessageModel<PageModel<ListBlogViewModel>>> Get(int pageIndex = 1, int pageSize = 20)
        {
            var blogs = await service.GetBlogList(pageIndex, pageSize);
            return new MessageModel<PageModel<ListBlogViewModel>>
            {
                response = blogs
            };
        }


        /// <summary>
        /// 根据id 返回 帖子内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public async Task<MessageModel<PostBlogViewModel>> Get(int id)
        {
            var blog = await service.Get(id);
            return new MessageModel<PostBlogViewModel>
            {
                response = blog
            };
        }

        /// <summary>
        /// 新增一个 blog
        /// </summary>
        /// <param name="blog"></param>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromBody] PostBlogViewModel blog)
        {
            return new MessageModel<int>
            {
                response = await service.Save(blog)

            };

        }

        /// <summary>
        /// 修改blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        // PUT api/<BlogController>/5
        [HttpPut]
        public async Task<MessageModel<int>> Put([FromBody] PostBlogViewModel blog)
        {
            return new MessageModel<int>
            {
                response = await service.Save(blog)

            };
        }

        /// <summary>
        /// DELETE api/[BlogController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<MessageModel<bool>> Delete(int id)
        {
            var result = await service.DeleteById(id);
            return new MessageModel<bool>
            {
                response = result
            };
        }
    }
}
