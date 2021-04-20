using System;
using System.Linq.Expressions;
using XjjXmm.Core.Database.Interface.Command;

namespace XjjXmm.Core.Database.Interface.Operate
{
    public interface IDeleteable<T>  : IWriteableCommand
    {
        IDeleteable<T> Where(Expression<Func<T, bool>> predicate);

        IDeleteable<T> Where(string whereExpression);

        IDeleteable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate);
    }
}
