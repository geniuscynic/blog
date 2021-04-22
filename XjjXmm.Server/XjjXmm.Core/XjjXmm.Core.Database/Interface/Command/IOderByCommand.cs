﻿using System;
using System.Linq.Expressions;
using System.Text;

namespace DoCare.Zkzx.Core.Database.Interface.Command
{
    interface IOrderByCommand
    {
        void AscBy<T, TResult>(Expression<Func<T, TResult>> predicate);

        void AscBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void AscBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void AscBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        void DescBy<T, TResult>(Expression<Func<T, TResult>> predicate);

        void DescBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void DescBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void DescBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        StringBuilder Build(bool ignorePrefix = true);
    }

    interface IOrderByCommand<T>
    {
        void AscBy<TResult>(Expression<Func<T, TResult>> predicate);

        void DescBy<TResult>(Expression<Func<T, TResult>> predicate);


        StringBuilder Build(bool ignorePrefix = true);
    }
}
