using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Permission.Model;
using Permission.Service;

using XjjXmm.Service.Permission;

namespace Permission.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public UserService UserService { get; set; }

        [HttpGet]
        public async Task<PageModel<UserModel>> Get(string name, int pageIndex = 1, int pageSize=10)
        {
            return await UserService.GetUsers(name, pageIndex, pageSize);
        }

        [HttpPost("/User/Login")]
        public async Task<UserDetailModel> FindUser(LoginModel loginModel)
        {
            return await UserService.FindUser(loginModel);
        }

        [HttpGet("/User/Detail")]
        public async Task<UserDetailModel> Detail(string id)
        {
            return await UserService.FindUser(id);
        }

        [HttpPost("/User/Add")]
        public async Task<bool> Add(AddUserModel model)
        {
            return await UserService.AddUser(model);
        }

        [HttpPost("/User/Edit")]
        public async Task<bool> Edit(EditUserModel model)
        {
            return await UserService.EditUser(model);
        }

        [HttpPost("/User/Delete")]
        public async Task<bool> Delete(string id)
        {
            return await UserService.SetUserStatus(id, Status.Delete);
        }

        [HttpPost("/User/Restore")]
        public async Task<bool> Restore(string id)
        {
            return await UserService.SetUserStatus(id, Status.Active);
        }
    }
}
