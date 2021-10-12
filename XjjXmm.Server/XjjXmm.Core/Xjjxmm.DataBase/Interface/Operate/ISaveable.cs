using System;
using System.Linq.Expressions;
using XjjXmm.DataBase.Interface.Command;

namespace XjjXmm.DataBase.Interface.Operate
{
   public interface ISaveable<T> : IWriteableCommand
    {
        ISaveable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate);

        ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate);

     
    }
}
