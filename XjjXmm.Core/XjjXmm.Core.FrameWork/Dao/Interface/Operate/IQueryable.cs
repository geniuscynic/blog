using System;
using System.Linq.Expressions;
using DoCare.Extension.Dao.Interface.Command;

namespace DoCare.Extension.Dao.Interface.Operate
{
    public interface IXXQueryable<T>  : IQueryCommand<T>
    {
        IXXQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IXXQueryable<T> Where(string whereExpression);

        IXXQueryable<T> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IQueryCommand<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate);
    }
}
