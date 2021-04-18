using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;

namespace Blog.IRepository
{
    public interface IButtonRepository : IRepository<Button>
    {
        Task<List<Button>> GetButtons(string token);

        Task<List<Button>> GetButtonsByRole(List<string> roles);
    }
}
