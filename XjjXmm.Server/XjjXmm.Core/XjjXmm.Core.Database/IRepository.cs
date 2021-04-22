using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DoCare.Zkzx.Core.Database
{
    public interface IRepository<T>
    {
        public Task<int> Add(T t);

        public Task<int> Add(IEnumerable<T> t);

        public Task<int> Save(T t);

        public Task<int> Save(IEnumerable<T> t);


        public Task<IEnumerable<T>> GetAll();

        public Task<T> Find(string id);

        public Task<IEnumerable<T>> Query(Expression<Func<T, bool>> whereExpression);


        public Task<T> SingleOrDefault(Expression<Func<T, bool>> whereExpression);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> whereExpression);
    }
}
