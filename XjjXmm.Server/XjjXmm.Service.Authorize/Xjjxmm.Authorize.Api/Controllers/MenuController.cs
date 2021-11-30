using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Authorization;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.Authorize.Api.Controllers
{
    [ApiController]
    [Route("api/menus")]
    [XjjXmmAuthorize]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        ///   获取前端所需菜单
        /// </summary>
        /// <param name=""></param>
        [HttpGet("build")]
        
        //[Authorize(Roles = "test")]
        public async Task<dynamic> BuildMenus()
        {
            var id = HttpContext?.User?.Identity?.Name ?? "0";
            var ids = int.Parse(id);

            var menuDtoList = await _menuService.FindByUser(ids);
            var menuDtos = _menuService.BuildTree(menuDtoList);
            // List<MenuDto> menuDtos = menuService.buildTree(menuDtoList);
            //return menuDtoList;
            return _menuService.buildMenus(menuDtos);
        }


    }
}
