using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.DataBase.Interface.Command;
using DoCare.Extension.DataBase.Utility;
using Org.BouncyCastle.Crypto.Modes.Gcm;

namespace DoCare.Extension.DataBase.Imp.Command
{
    public abstract class ReaderableCommand<T> : IReaderableCommand<T>
    {
        protected readonly IDbConnection _connection;
        protected readonly StringBuilder _sql;
        protected readonly Dictionary<string, object> _sqlParameter;
        protected readonly Aop _aop;


        public ReaderableCommand(IDbConnection connection, StringBuilder sql, Dictionary<string, object> sqlParameter, Aop aop)
        {
            _connection = connection;
            _sql = sql;
            _sqlParameter = sqlParameter;
            _aop = aop;
        }

        protected async Task<IEnumerable<T>> EnumerableDelegate(Func<Task<IEnumerable<T>>> func)
        {
            try
            {
                _aop?.OnExecuting?.Invoke(_sql.ToString(), _sqlParameter);

                var result = await func();

                _aop?.OnExecuted?.Invoke(_sql.ToString(), _sqlParameter);

                return result;
            }
            catch
            {
                _aop?.OnError?.Invoke(_sql.ToString(), _sqlParameter);
                throw;
            }
        }

        protected async Task<T> SingleDelegate(Func<Task<T>> func)
        {
            try
            {
                _aop?.OnExecuting?.Invoke(_sql.ToString(), _sqlParameter);

                var result = await func();

                _aop?.OnExecuted?.Invoke(_sql.ToString(), _sqlParameter);

                return result;
            }
            catch
            {
                _aop?.OnError?.Invoke(_sql.ToString(), _sqlParameter);
                throw;
            }
        }

        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            
            return await EnumerableDelegate(async () => await _connection.QueryAsync<T>(_sql.ToString(), _sqlParameter));
          
        }

        public async Task<T> ExecuteFirst()
        {
            return await SingleDelegate(async () => await _connection.QueryFirstAsync<T>(_sql.ToString(), _sqlParameter));
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            return await SingleDelegate(async () => await _connection.QueryFirstOrDefaultAsync<T>(_sql.ToString(), _sqlParameter));
        }

        public async Task<T> ExecuteSingle()
        {
            return await SingleDelegate(async () => await _connection.QuerySingleAsync<T>(_sql.ToString(), _sqlParameter));
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            return await SingleDelegate(async () => await _connection.QuerySingleOrDefaultAsync<T>(_sql.ToString(), _sqlParameter));
        }

        public abstract Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize);

        //public Task<T> ToPageList(int pageIndex, int pageSize)
        //{
        //    _sql.Append($" offset {pageIndex} rows fetch next {pageSize} rows only");
        //    _sql.Append($" limit {pageIndex}, {pageSize}");

        //    _sql.Append($"select ROWNUM rn, t.* from ({_sql}) t where rn < {(pageIndex + 1) * pageSize} ");
        //    _sql.Append($"select * from ({_sql}) t t.rn >= {pageIndex * pageSize}");
        //}
    }
}
