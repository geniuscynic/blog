using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.Dao.Interface.Command;

namespace DoCare.Extension.Dao.Imp.Operate
{
    class SimpleQueryable<T> : BaseSqlable<T>, IQueryCommand<T>
    {
       
        private readonly string _sql;
        private readonly Dictionary<string, object> _dynamicModel;


        public SimpleQueryable(IDbConnection connection, string sql, Dictionary<string, object> dynamicModel
        )    :base(connection)
        {
          
            _sql = sql;
            _sqlPamater = dynamicModel;
        }


       

        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            var sql = _sql; ;

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QueryAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteFirst()
        {
            var sql = _sql;

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QueryFirstAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            var sql = _sql;

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QueryFirstOrDefaultAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteSingle()
        {
            var sql = _sql;

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QuerySingleAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            var sql = _sql;

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QuerySingleOrDefaultAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }
    }
}
