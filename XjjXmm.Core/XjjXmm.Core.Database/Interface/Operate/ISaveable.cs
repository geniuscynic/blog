using System;
using System.Linq.Expressions;
using DoCare.Zkzx.Core.Database.Interface.Command;

namespace DoCare.Zkzx.Core.Database.Interface.Operate
{
   public interface ISaveable<T> : IWriteableCommand
    {
        ISaveable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate);

        ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate);

     
    }
}
