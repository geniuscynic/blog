using System;
using System.Linq.Expressions;
using XjjXmm.DataBase.Interface.Command;

namespace XjjXmm.DataBase.Interface.Operate
{
    public interface IXjjXmmQueryable<T>  : IReaderableCommand<T>
    {
        IXjjXmmQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IXjjXmmQueryable<T> Where(string whereExpression);

        IXjjXmmQueryable<T> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IXjjXmmQueryable<T> OrderBy<TResult>(Expression<Func<T, TResult>> predicate);

        IXjjXmmQueryable<T> OrderByDesc<TResult>(Expression<Func<T, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate);

        
    }
}
