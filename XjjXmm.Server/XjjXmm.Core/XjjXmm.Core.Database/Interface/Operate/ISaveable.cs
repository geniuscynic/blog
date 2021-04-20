using System;
using System.Linq.Expressions;
using XjjXmm.Core.Database.Interface.Command;

namespace XjjXmm.Core.Database.Interface.Operate
{
   public interface ISaveable<T> : IWriteableCommand
    {
        ISaveable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate);

        ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate);

     
    }
}
