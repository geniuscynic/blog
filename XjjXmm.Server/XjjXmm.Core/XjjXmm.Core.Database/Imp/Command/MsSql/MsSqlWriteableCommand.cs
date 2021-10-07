using System;
using System.Collections.Generic;
using Dapper;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command.MsSql
{
    internal class MsSqlWriteableCommand : WriteableCommand
    {
        public MsSqlWriteableCommand(DbInfo dbInfo, string sql, Dictionary<string, object> sqlParameter) : base(dbInfo,
            sql, sqlParameter)
        {
        }

        public MsSqlWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }

        protected override SqlMapper.ICustomQueryParameter BuildBigTextParamter(string val)
        {
            throw new NotImplementedException();
        }
    }
}
