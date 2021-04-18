using System;
using System.Linq.Expressions;
using DoCare.Zkzx.Core.Database.Interface.Command;

namespace DoCare.Zkzx.Core.Database.Interface.Operate
{
    public interface IDeleteable<T>  : IWriteableCommand
    {
        IDeleteable<T> Where(Expression<Func<T, bool>> predicate);

        IDeleteable<T> Where(string whereExpression);

        IDeleteable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate);
    }
}
