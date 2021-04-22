using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command
{
    public class WriteableCommand : IWriteableCommand 
    {
        private readonly IDbConnection _connection;
        private readonly string _sql;
        private readonly Dictionary<string, object> _sqlParameter;
        private readonly Aop _aop;


        public WriteableCommand(IDbConnection connection, string sql, Dictionary<string, object> sqlParameter, Aop aop)
        {
            _connection = connection;
            _sql = sql;
            _sqlParameter = sqlParameter;
            _aop = aop;
        }

        public async Task<int> Execute()
        {
            //var sql = Build().ToString();

            try
            {
                _aop?.OnExecuting?.Invoke(_sql, _sqlParameter);

                var result = await _connection.ExecuteAsync(_sql, _sqlParameter);

                _aop?.OnExecuted?.Invoke(_sql, _sqlParameter);

                return result;
            }
            catch
            {
                _aop?.OnError?.Invoke(_sql, _sqlParameter);
                throw;
            }
        }
    }
}
