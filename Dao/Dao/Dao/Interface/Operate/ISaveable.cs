using System;
using System.Linq.Expressions;
using ConsoleApp1.Dao.Interface.Command;

namespace ConsoleApp1.Dao.Interface.Operate
{
    public interface ISaveable<T> : IExecuteCommand
    {
        ISaveable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate);

        ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate);

     
    }
}
