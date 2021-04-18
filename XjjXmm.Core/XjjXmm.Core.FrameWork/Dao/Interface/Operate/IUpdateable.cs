using System;
using System.Linq.Expressions;
using DoCare.Extension.Dao.Interface.Command;

namespace DoCare.Extension.Dao.Interface.Operate
{
    public interface IUpdateable<T>  : IExecuteCommand
    {
        IUpdateable<T> SetColumns<TResult>(Expression<Func<TResult>> predicate);

        IUpdateable<T> Where(Expression<Func<T, bool>> predicate);

        IUpdateable<T> Where(string whereExpression);

        IUpdateable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate);
    }
}
