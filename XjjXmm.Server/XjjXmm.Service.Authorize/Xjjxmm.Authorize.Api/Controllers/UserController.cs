using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.Jwt;
using XjjXmm.FrameWork.LogExtension;

namespace XjjXmm.Authorize.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILog<UserController> _logger;

        public UserController(UserService userService, ILog<UserController> _logger)
        {
            _userService = userService;
            this._logger = _logger;
        }

        [HttpPost("/user/login")]
        public string login()
        {
            var jwtTokenSetting = App.GetJwtConfig();
            var jwtStr = JwtHelper.IssueToken(jwtTokenSetting, new TokenModelOptions()
            {
                AppId = "xjjxmm",
                ClientId = "admin",
                Id = "1",
                Roles = new List<string>() {"admin", "a2"}
            });

            _logger.Debug($"jwt:{jwtStr}");

            return jwtStr;
        }

        [HttpPost("/user/Add")]
        public async Task<bool> Add(AddUserModel model)
        {
            return await _userService.AddUser(model);
        }

        [HttpPost("/user/Edit")]
        public async Task<bool> Edit(EditUserModel model)
        {
            return await _userService.EditUser(model);
        }


        [HttpPost("/user/list")]
        public async Task<PageModel<UserModel>> List(SearchUserModel model)
        {
            return await _userService.GetUsers(model);
        }

        //[HttpPost("/user/login")]
        //public async Task<UserDetailModel> Login(LoginModel loginModel)
        //{
        //    return await _userService.FindUser(loginModel);
        //}

        [HttpGet("/user/detail")]
        public async Task<UserDetailModel> GetUserById(string id)
        {
            return await _userService.FindUser(id);
        }

        [HttpPost("/user/delete")]
        public async Task<bool> Delete(string id)
        {
            return await _userService.SetUserStatus(id, UserStatus.Delete);
        }

        [HttpPost("/user/restore")]
        public async Task<bool> Restore(string id)
        {
            return await _userService.SetUserStatus(id, UserStatus.Active);
        }
    }
}
