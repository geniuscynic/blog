using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Auth.Output;
using Admin.Core.Service.Admin.User.Input;
using Admin.Core.Service.Admin.User.Output;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.User
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [ProcessLog]
    public interface IUserService
    {
        Task<AuthLoginOutput> GetLoginUserAsync(long id);

        Task<object> GetAsync(long id);

        Task<object> GetSelectAsync();

        Task<PageOutput<UserListOutput>> PageAsync(PageInput<UserEntity> input);

        Task<bool> AddAsync(UserAddInput input);

        Task<bool> UpdateAsync(UserUpdateInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> BatchSoftDeleteAsync(long[] ids);

        Task<bool> ChangePasswordAsync(UserChangePasswordInput input);

        Task<bool> UpdateBasicAsync(UserUpdateBasicInput input);

        Task<UserUpdateBasicInput> GetBasicAsync();

        Task<IList<UserPermissionsOutput>> GetPermissionsAsync();
    }
}