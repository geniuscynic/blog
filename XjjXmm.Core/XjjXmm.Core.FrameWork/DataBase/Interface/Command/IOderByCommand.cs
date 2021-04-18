using System;
using System.Linq.Expressions;
using System.Text;

namespace DoCare.Extension.DataBase.Interface.Command
{
    interface IOrderByCommand
    {
        void OrderBy<T, TResult>(Expression<Func<T, TResult>> predicate);

        void OrderBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void OrderBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void OrderBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        void OrderByDesc<T, TResult>(Expression<Func<T, TResult>> predicate);

        void OrderByDesc<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        StringBuilder Build(bool ignorePrefix = true);
    }

    interface IOrderByCommand<T>
    {
        void OrderBy<TResult>(Expression<Func<T, TResult>> predicate);

        void OrderByDesc<TResult>(Expression<Func<T, TResult>> predicate);


        StringBuilder Build(bool ignorePrefix = true);
    }
}
