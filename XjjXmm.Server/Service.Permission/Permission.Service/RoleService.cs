using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using DoCare.Zkzx.Core.FrameWork.Tool.ToolKit;
using log4net.Util;
using Permission.Entity;
using Permission.IRepository;
using Permission.Model;
using Permission.Repository;
using XjjXmm.Core.FrameWork.Interceptor;
using XjjXmm.Core.FrameWork.Mapper;

namespace Permission.Service
{
   
    public class RoleService //: IRoleService
    {

        public IRoleRepository RoleRepository { get; set; }


        public async Task<IEnumerable<RoleModel>> GetRoleByUserId(string userId)
        {
            var results = await RoleRepository.GetRoleByUserId(userId);
            return results.MapTo<RoleEntity, RoleModel>();
        }
    }
}
