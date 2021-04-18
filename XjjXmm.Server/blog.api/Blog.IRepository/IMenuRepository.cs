using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;

namespace Blog.IRepository
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<List<Menu>> GetMenuForSuperAdmin();

        Task<List<Menu>> GetMenuByRole(List<string> role);
    }
}
