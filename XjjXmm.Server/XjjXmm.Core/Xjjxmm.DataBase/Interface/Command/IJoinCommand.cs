using System;
using System.Linq.Expressions;
using System.Text;

namespace XjjXmm.DataBase.Interface.Command
{
    interface IJoinCommand
    {
        void Join<T1, T2>(Expression<Func<T1, T2, bool>> predicate);
        void Join<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate);
        void Join<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void LeftJoin<T1, T2>(Expression<Func<T1, T2, bool>> predicate);
        void LeftJoin<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate);
        void LeftJoin<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate);

        StringBuilder Build<TJoin>();
    }


    interface IJoinCommand<T1, T2>
    {
        void Join(Expression<Func<T1, T2, bool>> predicate);

         StringBuilder Build();
    }

    interface IJoinCommand<T1, T2, T3> : IJoinCommand<T1, T2>
    {
        void Join(Expression<Func<T1, T2,T3,  bool>> predicate);

      
    }
}
