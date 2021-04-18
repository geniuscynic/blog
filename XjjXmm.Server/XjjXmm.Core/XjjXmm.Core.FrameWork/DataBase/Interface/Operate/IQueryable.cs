using System;
using System.Linq.Expressions;
using DoCare.Extension.DataBase.Interface.Command;

namespace DoCare.Extension.DataBase.Interface.Operate
{
    public interface IDoCareQueryable<T>  : IReaderableCommand<T>
    {
        IDoCareQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IDoCareQueryable<T> Where(string whereExpression);

        IDoCareQueryable<T> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IDoCareQueryable<T> OrderBy<TResult>(Expression<Func<T, TResult>> predicate);

        IDoCareQueryable<T> OrderByDesc<TResult>(Expression<Func<T, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate);

        
    }
}
