using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Repository
{
    public class Dbcontext
    {
        private readonly ISqlSugarClient _db;

        public Dbcontext(ISqlSugarClient db)
        {
            this._db = db;
        }

        public ISqlSugarClient Db
        {
            get { return _db; }
        }

        public SimpleClient<T> GetSimpleClient<T>() where T : class, new()
        {
            return _db.GetSimpleClient<T>();
        }

      
    }
}
