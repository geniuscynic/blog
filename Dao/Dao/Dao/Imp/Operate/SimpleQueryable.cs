using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Interface.Command;
using Dapper;

namespace ConsoleApp1.Dao.Imp.Operate
{
    class SimpleQueryable<T> : IQueryCommand<T>
    {
        private readonly IDbConnection _connection;
        private readonly StringBuilder _sql;
        private readonly Dictionary<string, object> _dynamicModel;


        public SimpleQueryable(IDbConnection connection, StringBuilder sql, Dictionary<string, object> dynamicModel
        )
        {
            _connection = connection;
            _sql = sql;
            _dynamicModel = dynamicModel;
        }


        public async Task<List<T>> ToList()
        {
            var result = await _connection.QueryAsync<T>(_sql.ToString(), _dynamicModel);

            return result.ToList();
        }
    }
}
