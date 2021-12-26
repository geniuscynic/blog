namespace Admin.Repository.Organization
{
    public partial interface IOrganizationRepository : IRepositoryBase<OrganizationEntity>
    {
        Task<List<OrganizationEntity>> GetList(string key);
    }
}