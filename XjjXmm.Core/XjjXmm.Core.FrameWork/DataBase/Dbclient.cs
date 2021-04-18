using System;
using System.Collections.Generic;
using System.Data;
using DoCare.Extension.DataBase.Imp.Operate;
using DoCare.Extension.DataBase.Interface.Operate;
using DoCare.Extension.DataBase.Utility;
using log4net;
using Newtonsoft.Json;

namespace DoCare.Extension.DataBase
{
    public class Dbclient : IDisposable
    {

        //public ILog logger { get; set; }

        private readonly IDbConnection _connection;

        public Aop Aop { get; set; }
        public Dbclient(string connectionString, string provider)
        {
            _connection = DatabaseFactory.CreateConnection(connectionString, provider);

            //Aop = new Aop()
            //{
            //    OnError = (sql, paramter) =>
            //    {
                    
            //        logger?.Error($"Sql:  {sql}, \r\n paramter: {JsonConvert.SerializeObject(paramter)}");
            //        //Console.WriteLine(sql);
            //    },
            //    OnExecuting = (sql, paramter) =>
            //    {
            //        //Console.WriteLine(sql);
            //        logger?.InfoFormat("Sql: \r\n{0}", sql);
            //    },

            //};
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


        public IDoCareQueryable<T> Queryable<T>()
        {
            return DatabaseFactory.CreateQueryable<T>(_connection, Aop);
        }

        public SimpleQueryable<T> Queryable<T>(string sql)
        {
            return Queryable<T>(sql, new Dictionary<string, object>());
        }

        public SimpleQueryable<T> Queryable<T>(string sql, Dictionary<string, object> sqlParameter)
        {
            return new SimpleQueryable<T>(_connection, sql, sqlParameter)
            {
                Aop = Aop
            };
        }

        public IComplexQueryable<T> ComplexQueryable<T>(string alias)
        {
            return DatabaseFactory.CreateComplexQueryable<T>(_connection, Aop, alias);
        }


        public IDeleteable<T> Deleteable<T>()
        {
            return DatabaseFactory.CreateDeleteable<T>(_connection, Aop);
        }

        

        public IDbConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
