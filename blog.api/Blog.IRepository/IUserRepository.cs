using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Models;

namespace Blog.IRepository
{
    public interface IUserRepository  : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
