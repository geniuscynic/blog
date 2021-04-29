using System;
using System.Collections.Generic;
using System.Data;
using DoCare.Zkzx.Core.Database.Imp.Operate;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database
{
    public class Dbclient : IDisposable
    {

        private DbInfo _builder;

      
        public Dbclient(string connectionString, string provider, Aop aop = null)
        {
            _builder = new DbInfo(connectionString, provider, aop);
        }

        public Dbclient(string connectionString, DatabaseProvider provider, Aop aop = null)
        {
            _builder = new DbInfo(connectionString, provider, aop);
        }

        public IInsertable<T> Insertable<T>(T model)
        {
            return DatabaseFactory.CreateInsertable<T, T>(_builder, model);
        }

        public IInsertable<T> Insertable<T>(IEnumerable<T> model)
        {
            return DatabaseFactory.CreateInsertable<T, IEnumerable<T>>(_builder, model);
        }

        public ISaveable<T> Saveable<T>(T model)
        {
            return DatabaseFactory.CreateSaveable<T, T>(_builder, model);
        }

        public ISaveable<T> Saveable<T>(List<T> model)
        {
            return DatabaseFactory.CreateSaveable<T, List<T>>(_builder, model);
        }

        public IUpdateable<T> Updateable<T>()
        {
            return DatabaseFactory.CreateUpdateable<T>(_builder.Connection, _builder.Aop);
        }


        public IDoCareQueryable<T> Queryable<T>()
        {
            return DatabaseFactory.CreateQueryable<T>(_builder.Connection, _builder.Aop);
        }

        public SimpleQueryable<T> Queryable<T>(string fullSql)
        {
            return Queryable<T>(fullSql, new Dictionary<string, object>());
        }

        public SimpleQueryable<T> Queryable<T>(string fullSql, Dictionary<string, object> sqlParameter)
        {
            return new SimpleQueryable<T>(_builder.Connection, fullSql, sqlParameter)
            {
                Aop = _builder.Aop
            };
        }

        public IComplexQueryable<T> ComplexQueryable<T>(string alias)
        {
            return DatabaseFactory.CreateComplexQueryable<T>(_builder.Connection, _builder.Aop, alias);
        }


        public IDeleteable<T> Deleteable<T>()
        {
            return DatabaseFactory.CreateDeleteable<T>(_builder.Connection, _builder.Aop);
        }

        
        public IDbTransaction BeginTransaction()
        {
            return _builder.Connection.Value.BeginTransaction();
        }

        //public ISqlFunc SqlFunc => DatabaseFactory.CreateSqlFunc(_connection);


        public IDbConnection GetConnection()
        {
            return _builder.Connection.Value;
        }

        public void Dispose()
        {
            _builder.Connection?.Value.Dispose();
        }
    }
}
