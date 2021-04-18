using System;
using System.Linq.Expressions;
using System.Text;

namespace DoCare.Zkzx.Core.Database.Interface.Command
{
    interface IWhereCommand
    {
        void Where<T1>(Expression<Func<T1, bool>> predicate);

        void Where<T1, T2>(Expression<Func<T1, T2, bool>> predicate);

        void Where<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate);

        void Where<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void Where(string whereExpression);

        void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        StringBuilder Build(bool ignorePrefix = true);
    }

    interface IWhereCommand<T>
    {
        void Where(Expression<Func<T, bool>> predicate);

        void Where(string whereExpression);

        void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        StringBuilder Build(bool ignorePrefix = true);
    }

    interface IWhereCommand<T1, T2> : IWhereCommand<T1>
    {
        void Where(Expression<Func<T1, T2,  bool>> predicate);
    }

    interface IWhereCommand<T1, T2, T3> : IWhereCommand<T1, T2>
    {
        void Where(Expression<Func<T1, T2, T3, bool>> predicate);
    }
}
