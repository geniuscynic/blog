using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.Authorize.Api.Controllers
{
    [ApiController]
    [Route("api/menus")]
    public class MenuController : ControllerBase
    {
        private MenuService menuService;

        /// <summary>
        ///   获取前端所需菜单
        /// </summary>
        /// <param name=""></param>
        [HttpGet("/build")]
   
    public ResponseEntity<Object> buildMenus()
    {
        List<MenuDto> menuDtoList = menuService.findByUser(SecurityUtils.getCurrentUserId());
        List<MenuDto> menuDtos = menuService.buildTree(menuDtoList);
        return new ResponseEntity<>(menuService.buildMenus(menuDtos), HttpStatus.OK);
    }

   
    }
}
