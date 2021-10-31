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
        Task<IEnumerable<dynamic>> ExecuteQuery();
        Task<IEnumerable<object>> ExecuteQuery(Type type);
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


        //Task<IEnumerable<T>> ExecuteQuery<T2, TSplit>(Expression<Func<T, T2, TSplit>> splitOnPredicate);

        //Task<T> ExecuteFirst<T2, TSplit>(Expression<Func<T, T2, TSplit>> splitOnPredicate);

        //Task<T> ExecuteFirstOrDefault<T2, TSplit>(Expression<Func<T, T2, TSplit>> splitOnPredicate);
    }

    public interface IReaderableCommand<T1, T2> : IReaderableCommand<T1>
    {

        //Task<IEnumerable<T1>> ExecuteQuery<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirst<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirstOrDefault<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate);

   /*     Task<IEnumerable<T1>> ExecuteQuery<TSplit>(Expression<Func<T1, T2, TSplit>> splitOnPredicate);

        Task<T1> ExecuteFirst<TSplit>(Expression<Func<T1, T2, TSplit>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TSplit>(Expression<Func<T1, T2, TSplit>> splitOnPredicate);*/

    }

    public interface IReaderableCommand<T1, T2, T3> : IReaderableCommand<T1, T2>
    {

        //Task<IEnumerable<T1>> ExecuteQuery<TSecond, TThird, TSplit>(Expression<Func<T1, TSecond, TThird, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirst<TSecond, TThird, TSplit>(Expression<Func<T1, TSecond, TThird, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirstOrDefault<TSecond, TThird, TSplit>(Expression<Func<T1, TSecond, TThird, TSplit>> splitOnPredicate);

       /* Task<IEnumerable<T1>> ExecuteQuery<TResult>(Expression<Func<T1, T2, T3, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Expression<Func<T1, T2, T3, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Expression<Func<T1, T2, T3, TResult>> splitOnPredicate);*/


    }

    public interface IReaderableCommand<T1, T2, T3, T4> : IReaderableCommand<T1,T2,T3>
    {
        //Task<IEnumerable<T1>> ExecuteQuery<TSecond, TThird, TFourth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirst<TSecond, TThird, TFourth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirstOrDefault<TSecond, TThird, TFourth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TSplit>> splitOnPredicate);

      /*  Task<IEnumerable<T1>> ExecuteQuery<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate);*/

    }


    public interface IReaderableCommand<T1, T2, T3, T4, T5> : IReaderableCommand<T1, T2, T3, T4>
    {
        //Task<IEnumerable<T1>> ExecuteQuery<TSecond, TThird, TFourth, TFifth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirst<TSecond, TThird, TFourth, TFifth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirstOrDefault<TSecond, TThird, TFourth, TFifth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSplit>> splitOnPredicate);

     /*   Task<IEnumerable<T1>> ExecuteQuery<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate);*/

    }

    public interface IReaderableCommand<T1, T2, T3, T4, T5, T6> : IReaderableCommand<T1, T2, T3, T4, T5>
    {
        //Task<IEnumerable<T1>> ExecuteQuery<TSecond, TThird, TFourth, TFifth, TSixth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSixth, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirst<TSecond, TThird, TFourth, TFifth, TSixth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSixth, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirstOrDefault<TSecond, TThird, TFourth, TFifth, TSixth, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSixth, TSplit>> splitOnPredicate);

      /*  Task<IEnumerable<T1>> ExecuteQuery<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate);*/

    }

    public interface IReaderableCommand<T1, T2, T3, T4, T5, T6, T7> : IReaderableCommand<T1, T2, T3, T4, T5, T6>
    {
        //Task<IEnumerable<T1>> ExecuteQuery<TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirst<TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirstOrDefault<TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TSplit>(Expression<Func<T1, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TSplit>> splitOnPredicate);


      /*  Task<IEnumerable<T1>> ExecuteQuery<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirst<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate);

        Task<T1> ExecuteFirstOrDefault<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate);*/

    }


    public interface ICommandBuilder
    {
        IReaderableCommand<T> Build<T>(StringBuilder sql, Dictionary<string, object> sqlParameter);
    }
}
