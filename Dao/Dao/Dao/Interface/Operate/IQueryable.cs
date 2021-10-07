using System;
using System.Linq.Expressions;
using ConsoleApp1.Dao.Interface.Command;

namespace ConsoleApp1.Dao.Interface.Operate
{
    public interface IXXQueryable<T>  : IQueryCommand<T>
    {
        IXXQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IXXQueryable<T> Where(string whereExpression);

        IXXQueryable<T> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IQueryCommand<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate);
    }
}
