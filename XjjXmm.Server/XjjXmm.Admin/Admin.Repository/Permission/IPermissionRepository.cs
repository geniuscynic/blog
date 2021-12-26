namespace Admin.Repository.Permission;

public interface IPermissionRepository : IRepositoryBase<PermissionEntity>
{
    Task<IEnumerable<PermissionEntity>> GetMenuPermissionByUserId(long userId);

    Task<IEnumerable<PermissionEntity>> GetDotPermissionByUserId(long userId);
    
}
