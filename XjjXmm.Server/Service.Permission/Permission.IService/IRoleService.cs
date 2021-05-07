using System.Collections.Generic;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Permission.Model;

namespace Permission.IService
{
    /// <summary>
    /// 角色 service
    /// </summary>
    public interface IRoleService
    {
        Task<BussinessModel<IEnumerable<RoleModel>>> GetRoleByUserId(string userId);

    }
}
