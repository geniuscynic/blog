using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Common;
using ConsoleApp1.Dao.Operate;
using ConsoleApp1.Dao.visitor;
using Dapper;

namespace ConsoleApp1.Dao.Command
{
    public class SimpleQueryable<T> : IQueryOperate<T>
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
