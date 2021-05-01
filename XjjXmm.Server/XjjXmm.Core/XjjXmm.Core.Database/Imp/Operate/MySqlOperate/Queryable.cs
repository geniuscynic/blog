﻿using System.Collections.Generic;
using System.Data;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command.MySql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlQueryable<T> : Queryable<T>
    {
        public MySqlQueryable(DbInfo dbInfo) : base(dbInfo)
        {
        }


        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo info, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MySqlReaderableCommand<TResult>(info, sql, sqlParameter);
        }

        
    }
}