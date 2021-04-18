using System;
using System.Linq.Expressions;
using DoCare.Extension.DataBase.Interface.Command;

namespace DoCare.Extension.DataBase.Interface.Operate
{
   public interface ISaveable<T> : IWriteableCommand
    {
        ISaveable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate);

        ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate);

     
    }
}
