using System;
using System.Linq.Expressions;
using DoCare.Extension.DataBase.Interface.Command;

namespace DoCare.Extension.DataBase.Interface.Operate
{
    public interface IUpdateable<T>  : IWriteableCommand
    {
        IUpdateable<T> SetColumns<TResult>(Expression<Func<TResult>> predicate);

        IUpdateable<T> Where(Expression<Func<T, bool>> predicate);

        IUpdateable<T> Where(string whereExpression);

        IUpdateable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate);
    }
}
