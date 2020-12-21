using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
 

	public class Dbcontext   : IDisposable
    {
        private readonly SqlConnection _connection;
        public Dbcontext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);

        }

        public Queryable<T> Queryable<T>()
        {
                return new Queryable<T>(_connection);
        }

        public Insertable<T> Insertable<T>(T model)
        {
            return new Insertable<T>(_connection, model);
        }

        public Updateable<T> Updateable<T>(T model)
        {
            return new Updateable<T>(_connection, model);
        }


        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
