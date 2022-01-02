using Admin.Core.Service.Admin.User;
using Admin.Service.User.Input;
using Admin.Service.User.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    public class UserController : ControllerBase
    {
       // private readonly IUser _user;
        //private readonly UploadConfig _uploadConfig;
        //private readonly UploadHelper _uploadHelper;
        private readonly IUserService _userService;

        public UserController(
           // IUser user,
            //IOptionsMonitor<UploadConfig> uploadConfig,
           // UploadHelper uploadHelper,
            IUserService userService
        )
        {
            //_user = user;
           // _uploadConfig = uploadConfig.CurrentValue;
           // _uploadHelper = uploadHelper;
            _userService = userService;
        }

        /// <summary>
        /// 查询用户基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserUpdateBasicInput> GetBasic()
        {
            return await _userService.GetBasic();
        }

        /// <summary>
        /// 查询单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> Get(long id)
        {
            return await _userService.Get(id);
        }

        /// <summary>
        /// 查询下拉数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetSelect()
        {
            return await _userService.GetSelect();
        }

        /// <summary>
        /// 查询分页用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        //[ResponseCache(Duration = 60)]
        public async Task<PageOutput<UserListOutput>> GetPage(PageInput<UserListInput> input)
        {
            return await _userService.Page(input);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(UserAddInput input)
        {
            return await _userService.Add(input);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(UserUpdateInput input)
        {
            return await _userService.Update(input);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _userService.SoftDelete(id);
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _userService.BatchSoftDelete(ids);
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> ChangePassword(UserChangePasswordInput input)
        {
            return await _userService.ChangePassword(input);
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateBasic(UserUpdateBasicInput input)
        {
            return await _userService.UpdateBasic(input);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        //[Login]
        public async Task<string> AvatarUpload([FromForm] IFormFile file)
        {
            //   var config = _uploadConfig.Avatar;
            // var res = await _uploadHelper.Upload(file, config, new { _user.Id });
            //if (res.Success)
            // {
            // return res.FileRelativePath;
            // }


            //return ResponseOutput.NotOk(res.Msg ?? "上传失败！");

            return "";
        }
    }
}