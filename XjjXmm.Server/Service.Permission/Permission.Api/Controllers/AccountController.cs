using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Permission.Model;
using Permission.Service;
using XjjXmm.Core.FrameWork.Common;
using XjjXmm.Service.Permission;

namespace Permission.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public AccountService AccountService { get; set; }

        [HttpGet]
        public async Task<BussinessModel<IEnumerable<UserModel>>> Get()
        {
            return await AccountService.GetUser();
        }

        [HttpPost("/User/Add")]
        public async Task<BussinessModel<UserModel>> Add(AddUserModel model)
        {
            return await AccountService.AddUser(model);
        }

        [HttpPost("/User/Edit")]
        public async Task<BussinessModel<UserModel>> Edit(EditUserModel model)
        {
            return await AccountService.EditUser(model);
        }

        [HttpPost("/User/Delete")]
        public async Task<BussinessModel<bool>> Delete(string id)
        {
            return await AccountService.SetUserStatus(id, Status.Delete);
        }

        [HttpPost("/User/Restore")]
        public async Task<BussinessModel<bool>> Restore(string id)
        {
            return await AccountService.SetUserStatus(id, Status.Active);
        }
    }
}
