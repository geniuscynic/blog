using Admin.Repository.Permission;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Repository
{
    public interface IRepositoryBase<T>  where T : class, new()
    {
        Task<long> Add(T entity);

        Task<List<long>> Add(List<T> entity);


        Task<bool> Update(T entity);

        Task<bool> Delete(dynamic id);

        Task<bool> Delete(Expression<Func<T, bool>> whereExpression);

       


        Task<bool> SoftDelete(dynamic id);

        Task<bool> SoftDelete(dynamic[] id);

       

        Task<T> GetById(dynamic id);

        Task<T> GetFirst(Expression<Func<T, bool>> whereExpression);

        
    }
}
