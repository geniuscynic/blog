using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper.XjjxmmHelper.Command;

namespace Dapper.XjjxmmHelper
{
 

	public class XjjxmmContext   : IDisposable
    {
        private readonly IDbConnection _connection;
        public XjjxmmContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);

        }

        public Queryable<T> Query<T>()
        {
                return new Queryable<T>(_connection);
        }

        public Insertable<T> Insert<T>(T model)
        {
            return new Insertable<T>(_connection, model);
        }

        public Updateable<T> Update<T>(T model)
        {
            return new Updateable<T>(_connection, model);
        }

        public Deleteable<T> Delete<T>()
        {
            return new Deleteable<T>(_connection);
        }

        public Deleteable<T> Delete<T>(T model)
        {
            return new Deleteable<T>(_connection, model);
        }


        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
