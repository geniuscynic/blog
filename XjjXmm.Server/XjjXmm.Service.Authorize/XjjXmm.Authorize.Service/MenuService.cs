using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.Authorize.Service.Vo;
using XjjXmm.FrameWork.Aop;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;
using XjjXmm.FrameWork.ToolKit;
using XjjXmm.FrameWork.ToolKit.DataEncryption.Encryptions;

namespace XjjXmm.Authorize.Service
{
    [Injection]
    public class MenuService// : IUserService
    {
        private readonly MenuRepository _menuRepository;
        private readonly RoleRepository _roleRepository;


        public MenuService(MenuRepository menuRepository, RoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
            _roleRepository = roleRepository;
        }

        [CustomInterceptor]
        public async virtual Task<IEnumerable<MenuDto>> FindByUser(long userId)
        {
            //var roles = await _roleRepository.FindByUserId(userId);

            //var roleIds = roles.Select(t => t.Id).ToList();

            //var menus = await _menuRepository.FindByRoleIdsAndTypeNot(roleIds, 2);

            var menus = await _menuRepository.FindByUserIdsAndTypeNot(userId, 2);

            return menus.MapTo<MenuEntity, MenuDto>();
        }

        public List<MenuDto> BuildTree(IEnumerable<MenuDto> menuDtos)
        {
            List<MenuDto> trees = new List<MenuDto>();
            var ids = new List<long>();

            foreach (var menuDto in menuDtos)
            {
                if (menuDto.Pid == null)
                {
                    trees.Add(menuDto);
                }

                foreach (var it in menuDtos)
                {
                    if (menuDto.Id == it.Pid)
                    {
                        if (menuDto.Children == null)
                        {
                            menuDto.Children = new List<MenuDto>();
                        }

                        menuDto.Children.Add(it);
                        ids.Add(it.Id);
                    }
                }
            }

            if (trees.Count == 0)
            {
                trees = menuDtos.Where(s => !ids.Contains(s.Id)).ToList();
            }

            return trees;
        }

        public List<MenuVo> buildMenus(List<MenuDto> menuDtos)
        {
            List<MenuVo> list = new List<MenuVo>();
            menuDtos.ForEach(menuDTO =>
            {
                if (menuDTO != null)
                {
                    List<MenuDto> menuDtoList = menuDTO.Children;
                    MenuVo menuVo = new MenuVo();
                    menuVo.Name = (!menuDTO.ComponentName.IsNullOrEmpty() ? menuDTO.ComponentName : menuDTO.Title);
                    // 一级目录需要加斜杠，不然会报警告
                    menuVo.Path = (menuDTO.Pid == null ? "/" + menuDTO.Path : menuDTO.Path);
                    menuVo.Hidden = menuDTO.Hidden;
                    // 如果不是外链
                    if (!menuDTO.IFrame)
                    {
                        if (menuDTO.Pid == null)
                        {
                            menuVo.Component = (menuDTO.Component.IsNullOrEmpty() ? "Layout" : menuDTO.Component);
                            // 如果不是一级菜单，并且菜单类型为目录，则代表是多级菜单
                        }
                        else if (menuDTO.Type == 0)
                        {
                            menuVo.Component = (menuDTO.Component.IsNullOrEmpty()) ? "ParentView" : menuDTO.Component;
                        }
                        else if (!menuDTO.Component.IsNullOrEmpty())
                        {
                            menuVo.Component = menuDTO.Component;
                        }
                    }
                    menuVo.Meta = new MenuMetaVo(menuDTO.Title, menuDTO.Icon, !menuDTO.Cache);
                    if (menuDtoList?.Count > 0)
                    {
                        menuVo.AlwaysShow = true;
                        menuVo.Redirect = "noredirect";
                        menuVo.Children = buildMenus(menuDtoList);
                        // 处理是一级菜单并且没有子菜单的情况
                    }
                    else if (menuDTO.Pid == null)
                    {
                        MenuVo menuVo1 = new MenuVo();
                        menuVo1.Meta = menuVo.Meta;
                        // 非外链
                        if (!menuDTO.IFrame)
                        {
                            menuVo1.Path = "index";
                            menuVo1.Name = menuVo.Name;
                            menuVo1.Component = menuVo.Component;
                        }
                        else
                        {
                            menuVo1.Path = menuDTO.Path;
                        }
                        menuVo.Name = null;
                        menuVo.Meta = null;
                        menuVo.Component = "Layout";
                        List<MenuVo> list1 = new List<MenuVo>();
                        list1.Add(menuVo1);
                        menuVo.Children = list1;
                    }
                    list.Add(menuVo);
                }
            }
        );
            return list;
        }



    }
}
