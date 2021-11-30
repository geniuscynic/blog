using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XjjXmm.Authorize.Repository.Criteria;
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
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly DeptService _deptService;
        private readonly ILog<UserController> _logger;

        public UserController(UserService userService, DeptService deptService, ILog<UserController> _logger)
        {
            _userService = userService;
            _deptService = deptService;
            this._logger = _logger;
        }


        /// <summary>
        ///  查询用户
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="pageable"></param>
        /// <returns></returns>
        public async Task<PageModel<UserDto>> query(UserQueryCriteria criteria, Pageable pageable)
        {
            
            if (criteria.DeptId.HasValue)
            {
                criteria.DeptIds.Add(criteria.DeptId.Value);
                // 先查找是否存在子节点
                var data = await _deptService.FindByPid(criteria.Id);
                // 然后把子节点的ID都加入到集合中
                criteria.DeptIds.AddRange(await _deptService.GetDeptChildren(data));
            }
            // 数据权限
            List<Long> dataScopes = dataService.getDeptIds(_userService.FindUser(App.UserId));
            // criteria.getDeptIds() 不为空并且数据权限不为空则取交集
            if (!CollectionUtils.isEmpty(criteria.getDeptIds()) && !CollectionUtils.isEmpty(dataScopes))
            {
                // 取交集
                criteria.getDeptIds().retainAll(dataScopes);
                if (!CollectionUtil.isEmpty(criteria.getDeptIds()))
                {
                    return new ResponseEntity<>(userService.queryAll(criteria, pageable), HttpStatus.OK);
                }
            }
            else
            {
                // 否则取并集
                criteria.getDeptIds().addAll(dataScopes);
                return new ResponseEntity<>(userService.queryAll(criteria, pageable), HttpStatus.OK);
            }
            return new ResponseEntity<>(PageUtil.toPage(null, 0), HttpStatus.OK);
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
