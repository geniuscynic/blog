using System;
using System.Collections.Generic;
using Dapper;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command.MySql
{
    internal class MysqlWriteableCommand : WriteableCommand
    {
        public MysqlWriteableCommand(DbInfo dbInfo, string sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }

        public MysqlWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }

        protected override SqlMapper.ICustomQueryParameter BuildBigTextParamter(string val)
        {
            throw new NotImplementedException();
        }
    }

}
