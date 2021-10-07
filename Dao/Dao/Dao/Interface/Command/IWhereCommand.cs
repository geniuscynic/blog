using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ConsoleApp1.Dao.Interface.Operate;

namespace ConsoleApp1.Dao.Interface.Command
{
    interface IWhereCommand<T> : ISqlBuilder
    {
        void Where(Expression<Func<T, bool>> predicate);

        void Where(string whereExpression);

        void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

       
    }
}
