using System;
using System.Collections.Generic;
using System.Data;
using DoCare.Extension.Dao.Common;
using DoCare.Extension.Dao.Imp.Operate;
using DoCare.Extension.Dao.Interface.Operate;
using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace DoCare.Extension.Dao
{
    public class Dbclient : IDisposable
    {

        public ILog logger { get; set; }

        private readonly IDbConnection _connection;

        protected Aop Aop { get; set; }
        public Dbclient(string connectionString, string provider)
        {
            _connection = DatabaseFactory.CreateConnection(connectionString, provider);

            Aop = new Aop()
            {
                OnError = (sql, paramter) =>
                {
                    
                    logger.Error($"Sql:  {sql}, \r\n paramter: {JsonConvert.SerializeObject(paramter)}");
                    //Console.WriteLine(sql);
                },
                OnExecuting = (sql, paramter) =>
                {
                    //Console.WriteLine(sql);
                    logger.InfoFormat("Sql: \r\n{0}", sql);
                },

            };
        }

        public IInsertable<T> Insertable<T>(T model)
        {
            return DatabaseFactory.CreateInsertable<T, T>(_connection, model, Aop);
        }

        public IInsertable<T> Insertable<T>(IEnumerable<T> model)
        {
            return DatabaseFactory.CreateInsertable<T, IEnumerable<T>>(_connection, model, Aop);
        }

        public ISaveable<T> Saveable<T>(T model)
        {
            return DatabaseFactory.CreateSaveable<T, T>(_connection, model, Aop);
        }

        public ISaveable<T> Saveable<T>(List<T> model)
        {
            return DatabaseFactory.CreateSaveable<T, List<T>>(_connection, model, Aop);
        }

        public IUpdateable<T> Updateable<T>()
        {
            return DatabaseFactory.CreateUpdateable<T>(_connection, Aop);
        }


        public IXXQueryable<T> Queryable<T>()
        {
            return DatabaseFactory.CreateQueryable<T>(_connection, Aop);
        }


        public IDeleteable<T> Deleteable<T>()
        {
            return DatabaseFactory.CreateDeleteable<T>(_connection, Aop);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
