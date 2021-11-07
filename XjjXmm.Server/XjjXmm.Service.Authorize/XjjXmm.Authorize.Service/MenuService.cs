using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service.Model;
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

        public async Task<IEnumerable<MenuDto>> FindByUser(long userId)
        {
            var roles = await _roleRepository.FindByUserId(userId);

            var roleIds = roles.Select(t => t.Id).ToList();

            var menus = await _menuRepository.FindByRoleIdsAndTypeNot(roleIds, 2);

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


    }
}
