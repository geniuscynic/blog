using System;
using System.Linq.Expressions;

namespace DoCare.Extension.Dao.Interface.Command
{
    interface IWhereCommand<T> : ISqlBuilder
    {
        void Where(Expression<Func<T, bool>> predicate);

        void Where(string whereExpression);

        void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

       
    }
}
