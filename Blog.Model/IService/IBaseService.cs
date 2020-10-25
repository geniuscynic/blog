using Blog.Common.Extensions.AOP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    public interface IBaseService<TEntity> where TEntity : class
    {

        
        Task<int> Add(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> Delete(TEntity model);

        Task<bool> DeleteByIds(object[] ids);
    }
}
