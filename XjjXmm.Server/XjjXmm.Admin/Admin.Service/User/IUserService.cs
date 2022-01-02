using Admin.Repository.User;
using Admin.Service.Auth;
using Admin.Service.User.Input;
using Admin.Service.User.Output;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.User
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [ProcessLog]
    public interface IUserService
    {
        Task<AuthLoginOutput> GetLoginUser(long id);

        Task<object> Get(long id);

        Task<object> GetSelect();

        Task<PageOutput<UserListOutput>> Page(PageInput<UserListInput> input);

        Task<bool> Add(UserAddInput input);

        Task<bool> Update(UserUpdateInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);

        Task<bool> BatchSoftDelete(long[] ids);

        Task<bool> ChangePassword(UserChangePasswordInput input);

        Task<bool> UpdateBasic(UserUpdateBasicInput input);

        Task<UserUpdateBasicInput> GetBasic();

        Task<IList<UserPermissionsOutput>> GetPermissions();
    }
}