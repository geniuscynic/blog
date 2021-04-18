using System;
using System.Linq.Expressions;
using DoCare.Extension.Dao.Interface.Command;

namespace DoCare.Extension.Dao.Interface.Operate
{
    public interface ISaveable<T> : IExecuteCommand
    {
        ISaveable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate);

        ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate);

     
    }
}
