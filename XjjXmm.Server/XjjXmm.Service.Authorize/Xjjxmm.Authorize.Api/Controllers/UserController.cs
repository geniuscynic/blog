using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.Authorize.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
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

        [HttpPost("/user/login")]
        public async Task<UserDetailModel> Login(LoginModel loginModel)
        {
            return await _userService.FindUser(loginModel);
        }

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
