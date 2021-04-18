using System;
using System.Linq.Expressions;
using DoCare.Extension.Dao.Interface.Command;

namespace DoCare.Extension.Dao.Interface.Operate
{
    public interface IDeleteable<T>  : IExecuteCommand
    {
        IDeleteable<T> Where(Expression<Func<T, bool>> predicate);

        IDeleteable<T> Where(string whereExpression);

        IDeleteable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate);
    }
}
