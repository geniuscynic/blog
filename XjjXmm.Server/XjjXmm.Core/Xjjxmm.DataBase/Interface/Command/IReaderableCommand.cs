using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.DataBase.Interface.Command
{
    public interface IReaderableCommand
    {
        Task<IEnumerable<T>> ExecuteQuery<T>();

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2>(Func<T1, T2, T1> func, params string[] splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3>(Func<T1, T2, T3, T1> func, params string[] splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, params string[] splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, params string[] splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, params string[] splitOn);

        Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, params string[] splitOn);



        Task<T> ExecuteFirst<T>();

        Task<T> ExecuteFirstOrDefault<T>();

        Task<T> ExecuteSingle<T>();

        Task<T> ExecuteSingleOrDefault<T>();



        Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize);

        Task<DataTable> ExecuteDataTable<T>();
    }

    public interface IReaderableCommand<T>
    {
        Task<IEnumerable<T>> ExecuteQuery();

        Task<IEnumerable<T>> ExecuteQuery<T2>(Func<T, T2, T> func, params string[] splitOn);

        Task<IEnumerable<T>> ExecuteQuery<T2, T3>(Func<T, T2, T3, T> func, params string[] splitOn);

        Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4>(Func<T, T2, T3, T4, T> func, params string[] splitOn);

        Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5>(Func<T, T2, T3, T4, T5, T> func, params string[] splitOn);

        Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5, T6>(Func<T, T2, T3, T4, T5, T6, T> func, params string[] splitOn);

        Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5, T6, T7>(Func<T, T2, T3, T4, T5, T6, T7, T> func, params string[] splitOn);


        Task<T> ExecuteFirst();

        Task<T> ExecuteFirstOrDefault();

        Task<T> ExecuteSingle();

        Task<T> ExecuteSingleOrDefault();

        Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize);

        Task<DataTable> ExecuteDataTable();
    }

    public interface ICommandBuilder
    {
        IReaderableCommand<T> Build<T>(StringBuilder sql, Dictionary<string, object> sqlParameter);
    }
}
