using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command
{
    internal class WriteableCommand : IWriteableCommand 
    {
        //private readonly DbInfo _dbInfo;
        private readonly Lazy<IDbConnection> _connection;
        private readonly string _sql;
        private readonly object _sqlParameter;
        private readonly Aop _aop;


        public WriteableCommand(DbInfo dbInfo, string sql, Dictionary<string, object> sqlParameter):this(dbInfo, sql,(object)sqlParameter)
        {
            
        }

        public WriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            _aop = dbInfo.Aop;
            _connection = dbInfo.Connection;

            _sql = sql;
            _sqlParameter = sqlParameter;
           
        }

        public async Task<int> Execute()
        {
            //var sql = Build().ToString();

            try
            {
                _aop?.OnExecuting?.Invoke(_sql, _sqlParameter);

                var result = await _connection.Value.ExecuteAsync(_sql, _sqlParameter);

                _aop?.OnExecuted?.Invoke(_sql, _sqlParameter);

                return result;
            }
            catch(Exception ex)
            {
                _aop?.OnError?.Invoke(_sql, _sqlParameter);
                throw;
            }
        }
    }
}
