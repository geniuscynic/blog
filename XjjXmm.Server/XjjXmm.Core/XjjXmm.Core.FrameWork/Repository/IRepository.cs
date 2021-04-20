using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace XjjXmm.Core.FrameWork.Repository
{
    public interface IRepository<T>
    {
        public Task<int> Add(T t);

        public Task<int> Add(IEnumerable<T> t);


        public Task<T> Find(object id);

        public Task<IEnumerable<T>> Query(Expression<Func<T, bool>> whereExpression);


        public Task<T> SingleOrDefault(Expression<Func<T, bool>> whereExpression);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> whereExpression);
    }
}
