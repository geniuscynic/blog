using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Interface.Command;
using Xjjxmm.DataBase.Utility;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command
{
    internal abstract class InnerReaderableCommand : IReaderableCommand
    {
        //protected readonly List<string> _splitList;
        protected Lazy<IDbConnection> Connection { get; set; }
        protected StringBuilder Sql { get; set; }
        protected Dictionary<string, object> SqlParameter { get; set; }
        protected Aop Aop { get; set; }


        //protected ReaderableCommand()
        //{

        //}

        protected InnerReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            //_splitList = splitList;
            Connection = dbInfo.Connection;
            Sql = sql;
            SqlParameter = sqlParameter;
            Aop = dbInfo.Aop;
        }

        //protected abstract ReaderableCommand<T> CrateReaderableCommand();

        //public IReaderableCommand<T> Build(DbInfo dbInfo,  StringBuilder sql, Dictionary<string, object> sqlParameter)
        //{
        //    var command = CrateReaderableCommand();
        //    command.Aop = dbInfo.Aop;
        //    command.Connection = dbInfo.Connection;
        //    command.Sql = sql;
        //    command.SqlParameter = sqlParameter;

        //    return command;

        //}


        protected async Task<IEnumerable<T>> EnumerableDelegate<T>(Func<Task<IEnumerable<T>>> func)
        {
            try
            {
                Aop?.OnExecuting?.Invoke(Sql.ToString(), SqlParameter);

                var result = await func();

                Aop?.OnExecuted?.Invoke(Sql.ToString(), SqlParameter);

                return result;
            }
            catch (Exception ex)
            {

                Aop?.OnError?.Invoke(Sql.ToString(), SqlParameter, ex);
                throw;
            }
        }

        protected async Task<T> SingleDelegate<T>(Func<Task<T>> func)
        {
            try
            {
                Aop?.OnExecuting?.Invoke(Sql.ToString(), SqlParameter);

                var result = await func();

                Aop?.OnExecuted?.Invoke(Sql.ToString(), SqlParameter);

                return result;
            }
            catch (Exception ex)
            {
                Aop?.OnError?.Invoke(Sql.ToString(), SqlParameter, ex);
                throw;
            }
        }

        //public async Task<IEnumerable<dynamic>> ExecuteQuery()
        //{
        //    return await EnumerableDelegate(async () =>
        //        await Connection.Value.QueryAsync(Sql.ToString(), SqlParameter));
        //}

        //public async Task<IEnumerable<object>> ExecuteQuery(Type type)
        //{
        //    return await EnumerableDelegate(async () =>
        //        await Connection.Value.QueryAsync(type, Sql.ToString(), SqlParameter));
        //}

        public async Task<IEnumerable<T>> ExecuteQuery<T>()
        {

            return await EnumerableDelegate(async () =>
                await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));
        }

        //public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2>(Func<T1, T2, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    //var map = MappingCache<T1, T2>.Map;

        //    return await EnumerableDelegate(async () =>
        //        await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func, splitOn: splitOn));
        //}

      

        //public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3>(Func<T1, T2, T3, T1> func, string splitOn)
        //{
        //   // var tmp = string.Join(',', splitOn);

        //    return await EnumerableDelegate(async () =>
        //        await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func, splitOn: splitOn));
        //}

        //public async Task<IEnumerable<T1>> ExecuteQuery<T1,T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await EnumerableDelegate<T1>(async () =>
        //        await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func, splitOn: splitOn));
        //}

        //public async Task<IEnumerable<T1>> ExecuteQuery<T1,T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await EnumerableDelegate(async () =>
        //        await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func, splitOn: splitOn));
        //}

        //public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await EnumerableDelegate(async () =>
        //        await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func, splitOn: splitOn));
        //}

        //public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await EnumerableDelegate(async () =>
        //        await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func, splitOn: splitOn));
        //}

        public async Task<T1> ExecuteFirst<T1>()
        {
            return await SingleDelegate(async () =>
                await Connection.Value.QueryFirstAsync<T1>(Sql.ToString(), SqlParameter));
        }

        //public async Task<T1> ExecuteFirst<T1,T2>(Func<T1, T2, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.First();
        //    });
        //}

        //public async Task<T1> ExecuteFirst<T1, T2, T3>(Func<T1, T2, T3, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.First();
        //    });

        //}

        //public async Task<T1> ExecuteFirst<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.First();
        //    });
        //}

        //public async Task<T1> ExecuteFirst<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.First();
        //    });
        //}

        //public async Task<T1> ExecuteFirst<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.First();
        //    });
        //}

        //public async Task<T1> ExecuteFirst<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.First();
        //    });
        //}


        public async Task<T1> ExecuteFirstOrDefault<T1>()
        {
            return await SingleDelegate(async () =>
                await Connection.Value.QueryFirstOrDefaultAsync<T1>(Sql.ToString(), SqlParameter));
        }

        //public async Task<T1> ExecuteFirstOrDefault<T1, T2>(Func<T1, T2, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.FirstOrDefault();
        //    });
        //}

        //public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3>(Func<T1, T2, T3, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.FirstOrDefault();
        //    });
        //}

        //public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, string splitOn)
        //{
        //   // var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.FirstOrDefault();
        //    });
        //}

        //public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.FirstOrDefault();
        //    });
        //}

        //public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.FirstOrDefault();
        //    });
        //}

        //public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, string splitOn)
        //{
        //    //var tmp = string.Join(',', splitOn);
        //    return await SingleDelegate(async () =>
        //    {
        //        var res = await Connection.Value.QueryAsync(sql: Sql.ToString(), param: SqlParameter, map: func,
        //            splitOn: splitOn);

        //        return res.FirstOrDefault();
        //    });
        //}

        public async Task<T1> ExecuteSingle<T1>()
        {
            return await SingleDelegate(async () =>
                await Connection.Value.QuerySingleAsync<T1>(Sql.ToString(), SqlParameter));
        }

        public async Task<T1> ExecuteSingleOrDefault<T1>()
        {

            return await SingleDelegate(async () =>
                await Connection.Value.QuerySingleOrDefaultAsync<T1>(Sql.ToString(), SqlParameter));
        }


        public async Task<DataTable> ExecuteDataTable<T1>()
        {

            try
            {
                Aop?.OnExecuting?.Invoke(Sql.ToString(), SqlParameter);

                var reader = await Connection.Value.ExecuteReaderAsync(Sql.ToString(), SqlParameter);

                Aop?.OnExecuted?.Invoke(Sql.ToString(), SqlParameter);

                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {

                Aop?.OnError?.Invoke(Sql.ToString(), SqlParameter, ex);
                throw;
            }


        }

        public abstract Task<(IEnumerable<T1> data, int total)> ToPageList<T1>(int pageIndex, int pageSize);
       
    }

    //internal abstract class ReaderableCommandBuilder : ICommandBuilder
    //{
    //    protected DbInfo dbInfo;


    //    internal ReaderableCommandBuilder(DbInfo dbInfo)
    //    {
    //        this.dbInfo = dbInfo;
    //    }

    //    protected abstract ReaderableCommand<T> GetReaderableCommand<T>(StringBuilder sql, Dictionary<string, object> sqlParameter);

    //    public IReaderableCommand<T> Build<T>(StringBuilder sql, Dictionary<string, object> sqlParameter)
    //    {
    //        var command = GetReaderableCommand<T>(sql,sqlParameter);
    //        //command.Aop = dbInfo.Aop;
    //        //command.Connection = dbInfo.Connection;
    //        //command.Sql = sql;
    //        //command.SqlParameter = sqlParameter;

    //        return command;

    //    }
    //}

    //public abstract class ReaderableCommand : IReaderableCommand
    //{
    //    protected Lazy<IDbConnection> Connection { get; set; }
    //    protected StringBuilder Sql { get; set; }
    //    protected Dictionary<string, object> SqlParameter { get; set; }
    //    protected Aop Aop { get; set; }


    //    protected ReaderableCommand()
    //    {

    //    }

    //    protected async Task<IEnumerable<T>> EnumerableDelegate<T>(Func<Task<IEnumerable<T>>> func)
    //    {
    //        try
    //        {
    //            Aop?.OnExecuting?.Invoke(Sql.ToString(), SqlParameter);

    //            var result = await func();

    //            Aop?.OnExecuted?.Invoke(Sql.ToString(), SqlParameter);

    //            return result;
    //        }
    //        catch
    //        {
    //            Aop?.OnError?.Invoke(Sql.ToString(), SqlParameter);
    //            throw;
    //        }
    //    }

    //    protected async Task<T> SingleDelegate<T>(Func<Task<T>> func)
    //    {
    //        try
    //        {
    //            Aop?.OnExecuting?.Invoke(Sql.ToString(), SqlParameter);

    //            var result = await func();

    //            Aop?.OnExecuted?.Invoke(Sql.ToString(), SqlParameter);

    //            return result;
    //        }
    //        catch
    //        {
    //            Aop?.OnError?.Invoke(Sql.ToString(), SqlParameter);
    //            throw;
    //        }
    //    }

    //    public async Task<IEnumerable<T>> ExecuteQuery<T>()
    //    {

    //        return await EnumerableDelegate(async () =>
    //            await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

    //    }

    //    public async Task<T> ExecuteFirst<T>()
    //    {
    //        return await SingleDelegate(async () =>
    //            await Connection.Value.QueryFirstAsync<T>(Sql.ToString(), SqlParameter));
    //    }

    //    public async Task<T> ExecuteFirstOrDefault<T>()
    //    {
    //        return await SingleDelegate(async () =>
    //            await Connection.Value.QueryFirstOrDefaultAsync<T>(Sql.ToString(), SqlParameter));
    //    }

    //    public async Task<T> ExecuteSingle<T>()
    //    {
    //        return await SingleDelegate(async () =>
    //            await Connection.Value.QuerySingleAsync<T>(Sql.ToString(), SqlParameter));
    //    }

    //    public async Task<T> ExecuteSingleOrDefault<T>()
    //    {
    //        return await SingleDelegate(async () =>
    //            await Connection.Value.QuerySingleOrDefaultAsync<T>(Sql.ToString(), SqlParameter));
    //    }

    //    public abstract Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize);



    //    internal abstract class ReaderableCommandBuilder : ICommandBuilder
    //    {
    //        private DbInfo dbInfo;


    //        internal ReaderableCommandBuilder(DbInfo dbInfo)
    //        {
    //            this.dbInfo = dbInfo;
    //        }

    //        protected abstract ReaderableCommand GetReaderableCommand();

    //        public IReaderableCommand Build(StringBuilder sql, Dictionary<string, object> sqlParameter)
    //        {
    //            var command = GetReaderableCommand();
    //            command.Aop = dbInfo.Aop;
    //            command.Connection = dbInfo.Connection;
    //            command.Sql = sql;
    //            command.SqlParameter = sqlParameter;

    //            return command;

    //        }
    //    }

    //}

}
