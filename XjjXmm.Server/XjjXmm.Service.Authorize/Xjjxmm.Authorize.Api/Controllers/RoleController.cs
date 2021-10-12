using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;

namespace XjjXmm.Authorize.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("/role/Add")]
        public async Task<bool> Add(AddRoleModel model)
        {
            return await _roleService.Add(model);
        }
    }
}
