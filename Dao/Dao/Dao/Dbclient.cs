using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ConsoleApp1.Dao.Command;
using ConsoleApp1.Dao.Operate;

namespace ConsoleApp1.Dao
{
    public class Dbclient : IDisposable
    {
        private readonly SqlConnection _connection;
        public Dbclient(string connectionString)
        {
            _connection = new SqlConnection(connectionString);

        }

        public IInsertable<T> Insertable<T>(T model)
        {
            return new Insertable<T>(_connection, model);
        }

        public ISaveable<T> Saveable<T>(T model)
        {
            return new Saveable<T, T>(_connection, model);
        }

        public ISaveable<T> Saveable<T>(List<T> model)
        {
            return new Saveable<T, List<T>>(_connection, model);
        }

        public IUpdateable<T> Updateable<T>()
        {
            return new Updateable<T>(_connection);
        }


        public IXjjXmmQueryable<T> Queryable<T>()
        {
            return new Queryable<T>(_connection);
        }




        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
