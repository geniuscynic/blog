﻿using System.Data;

namespace XjjXmm.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlSaveable<T, TEntity>  : Saveable<T, TEntity>
    {
       

        public SqlSaveable(IDbConnection connection, TEntity model): base(connection, model)
        {
           
        }

    }
}
