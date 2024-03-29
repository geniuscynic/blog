﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Imp.Command.Oracle;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command
{
    internal abstract class ReaderableCommand<T> : IReaderableCommand<T>
    {
        protected Lazy<IDbConnection> Connection { get; set; }
        protected StringBuilder Sql { get; set; }
        protected Dictionary<string, object> SqlParameter { get; set; }
        protected Aop Aop { get; set; }


        //protected ReaderableCommand()
        //{

        //}

        protected ReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
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


        protected async Task<IEnumerable<T>> EnumerableDelegate(Func<Task<IEnumerable<T>>> func)
        {
            try
            {
                Aop?.OnExecuting?.Invoke(Sql.ToString(), SqlParameter);

                var result = await func();

                Aop?.OnExecuted?.Invoke(Sql.ToString(), SqlParameter);

                return result;
            }
            catch(Exception ex)
            {
               
                Aop?.OnError?.Invoke(Sql.ToString(), SqlParameter, ex);
                throw;
            }
        }

        protected async Task<T> SingleDelegate(Func<Task<T>> func)
        {
            try
            {
                Aop?.OnExecuting?.Invoke(Sql.ToString(), SqlParameter);

                var result = await func();

                Aop?.OnExecuted?.Invoke(Sql.ToString(), SqlParameter);

                return result;
            }
            catch(Exception ex)
            {
                Aop?.OnError?.Invoke(Sql.ToString(), SqlParameter, ex);
                throw;
            }
        }

        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            return await EnumerableDelegate(async () =>
                await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

        }

        public async Task<T> ExecuteFirst()
        {
            return await SingleDelegate(async () =>
                await Connection.Value.QueryFirstAsync<T>(Sql.ToString(), SqlParameter));
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            return await SingleDelegate(async () =>
                await Connection.Value.QueryFirstOrDefaultAsync<T>(Sql.ToString(), SqlParameter));
        }

        public async Task<T> ExecuteSingle()
        {
            return await SingleDelegate(async () =>
                await Connection.Value.QuerySingleAsync<T>(Sql.ToString(), SqlParameter));
        }

        public async Task<T> ExecuteSingleOrDefault()
        {

            return await SingleDelegate(async () =>
                await Connection.Value.QuerySingleOrDefaultAsync<T>(Sql.ToString(), SqlParameter));
        }


        public async Task<DataTable> ExecuteDataTable()
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

        public abstract Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize);

        //public Task<T> ToPageList(int pageIndex, int pageSize)
        //{
        //    _sql.Append($" offset {pageIndex} rows fetch next {pageSize} rows only");
        //    _sql.Append($" limit {pageIndex}, {pageSize}");

        //    _sql.Append($"select ROWNUM rn, t.* from ({_sql}) t where rn < {(pageIndex + 1) * pageSize} ");
        //    _sql.Append($"select * from ({_sql}) t t.rn >= {pageIndex * pageSize}");
        //}

        //internal abstract class ReaderableCommandBuilder : ICommandBuilder
        //{
        //    private DbInfo dbInfo;


        //    internal ReaderableCommandBuilder(DbInfo dbInfo)
        //    {
        //        this.dbInfo = dbInfo;
        //    }

        //    protected abstract ReaderableCommand<T1> GetReaderableCommand<T1>();

        //    public IReaderableCommand<T1> Build<T1>(StringBuilder sql, Dictionary<string, object> sqlParameter)
        //    {
        //        var command = GetReaderableCommand<T1>();
        //        command.Aop = dbInfo.Aop;
        //        command.Connection = dbInfo.Connection;
        //        command.Sql = sql;
        //        command.SqlParameter = sqlParameter;

        //        return command;

        //    }
        //}

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
