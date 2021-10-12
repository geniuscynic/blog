using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase
{
    public interface IRepository<T>
    {
        public Task<int> Add(T t);

        public Task<int> Add(IEnumerable<T> t);

        public Task<int> Save(T t);

        public Task<int> Save<TResult>(T t, Expression<Func<T, TResult>> ignoreColunm);

        public Task<int> Save(IEnumerable<T> t);

        public Task<int> Save<TResult>(IEnumerable<T> t, Expression<Func<T, TResult>> ignoreColunm);

        public Task<int> Update<TResult>(Expression<Func<TResult>> setColunmExpression, Expression<Func<T, bool>> whereExpression);


        public Task<int> Delete(Expression<Func<T, bool>> whereExpression);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> Find(string id);

        public Task<IEnumerable<T>> Query<TResult>(Expression<Func<T, TResult>> orderBy, OrderByType orderByType = OrderByType.ASC);


        public Task<IEnumerable<T>> Query(Expression<Func<T, bool>> whereExpression);

        public Task<IEnumerable<T>> Query<TResult>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TResult>> orderBy, OrderByType orderByType = OrderByType.ASC);

        public Task<T> SingleOrDefault(Expression<Func<T, bool>> whereExpression);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> whereExpression);
    }
}
