using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.DataBase.Interface.Command
{
    internal interface IReaderableCommand
    {
        Task<IEnumerable<T>> ExecuteQuery<T>();

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2>(Func<T1, T2, T1> func, string splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3>(Func<T1, T2, T3, T1> func, string splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, string splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, string splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, string splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, string splitOn);



        Task<T> ExecuteFirst<T>();

        Task<T1> ExecuteFirst<T1, T2>(Func<T1, T2, T1> func, string splitOn);

        Task<T1> ExecuteFirst<T1, T2, T3>(Func<T1, T2, T3, T1> func, string splitOn);

        Task<T1> ExecuteFirst<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, string splitOn);

        Task<T1> ExecuteFirst<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, string splitOn);

        Task<T1> ExecuteFirst<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, string splitOn);

        Task<T1> ExecuteFirst<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, string splitOn);



        Task<T> ExecuteFirstOrDefault<T>();

        Task<T1> ExecuteFirstOrDefault<T1, T2>(Func<T1, T2, T1> func, string splitOn);

        Task<T1> ExecuteFirstOrDefault<T1, T2, T3>(Func<T1, T2, T3, T1> func, string splitOn);

        Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, string splitOn);

        Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, string splitOn);

        Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, string splitOn);

        Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, string splitOn);



        Task<T> ExecuteSingle<T>();



        Task<T> ExecuteSingleOrDefault<T>();



        Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize);

        Task<DataTable> ExecuteDataTable<T>();
    }


    public interface IReaderableCommand<T>
    {
        Task<IEnumerable<T>> ExecuteQuery();

        Task<T> ExecuteFirst();


        Task<T> ExecuteFirstOrDefault();
        
        Task<T> ExecuteSingle();

        Task<T> ExecuteSingleOrDefault();

        Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize);

        Task<DataTable> ExecuteDataTable();
    }

    public interface IReaderableCommand<T1, T2> : IReaderableCommand<T1>
    {

        Task<IEnumerable<T1>> ExecuteQuery<TResult>(Func<T1, T2, T1> func, Expression<Func<T1, T2, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Func<T1, T2, T1> func, Expression<Func<T1, T2, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Func<T1, T2, T1> func, Expression<Func<T1, T2, TResult>> splitOnPredicate);

    }

    public interface IReaderableCommand<T1, T2, T3> : IReaderableCommand<T1, T2>
    {

        Task<IEnumerable<T1>> ExecuteQuery<TResult>(Func<T1, T2, T3, T1> func, Expression<Func<T1, T2, T3, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Func<T1, T2, T3, T1> func, Expression<Func<T1, T2, T3, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Func<T1, T2, T3, T1> func, Expression<Func<T1, T2, T3, TResult>> splitOnPredicate);


    }

    public interface IReaderableCommand<T1, T2, T3, T4> : IReaderableCommand<T1,T2,T3>
    {
        Task<IEnumerable<T1>> ExecuteQuery<TResult>(Func<T1, T2, T3, T4, T1> func, Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Func<T1, T2, T3, T4, T1> func, Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Func<T1, T2, T3, T4, T1> func, Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate);

    }


    public interface IReaderableCommand<T1, T2, T3, T4, T5> : IReaderableCommand<T1, T2, T3, T4>
    {
        Task<IEnumerable<T1>> ExecuteQuery<TResult>(Func<T1, T2, T3, T4, T5, T1> func, Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Func<T1, T2, T3, T4, T5, T1> func, Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Func<T1, T2, T3, T4, T5, T1> func, Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate);

    }

    public interface IReaderableCommand<T1, T2, T3, T4, T5, T6> : IReaderableCommand<T1, T2, T3, T4, T5>
    {
        Task<IEnumerable<T1>> ExecuteQuery<TResult>(Func<T1, T2, T3, T4, T5, T6, T1> func, Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Func<T1, T2, T3, T4, T5, T6, T1> func, Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Func<T1, T2, T3, T4, T5, T6, T1> func, Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate);

    }

    public interface IReaderableCommand<T1, T2, T3, T4, T5, T6, T7> : IReaderableCommand<T1, T2, T3, T4, T5, T6>
    {
        Task<IEnumerable<T1>> ExecuteQuery<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate);

    }


    public interface ICommandBuilder
    {
        IReaderableCommand<T> Build<T>(StringBuilder sql, Dictionary<string, object> sqlParameter);
    }
}
