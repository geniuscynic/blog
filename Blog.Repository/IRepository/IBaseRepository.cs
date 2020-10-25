using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<int> Add(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> Delete(TEntity model);

        Task<bool> DeleteByIds(object[] ids);
    }
}
