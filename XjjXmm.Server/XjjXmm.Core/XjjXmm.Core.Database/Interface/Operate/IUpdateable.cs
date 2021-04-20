using System;
using System.Linq.Expressions;
using XjjXmm.Core.Database.Interface.Command;

namespace XjjXmm.Core.Database.Interface.Operate
{
    public interface IUpdateable<T>  : IWriteableCommand
    {
        IUpdateable<T> SetColumns<TResult>(Expression<Func<TResult>> predicate);

        IUpdateable<T> Where(Expression<Func<T, bool>> predicate);

        IUpdateable<T> Where(string whereExpression);

        IUpdateable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate);
    }
}
