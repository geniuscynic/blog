using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Dao.Operate
{
    public interface IXjjXmmQueryable<T>  : IOperate
    {
        IXjjXmmQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IXjjXmmQueryable<T> Where(string whereExpression);

        IXjjXmmQueryable<T> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IXjjXmmQueryable<T> Select<TResult>(Expression<Func<TResult>> predicate);
    }
}
