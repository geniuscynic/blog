using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command.Oracle
{
    internal class OracleWriteableCommand : WriteableCommand
    {
        public OracleWriteableCommand(DbInfo dbInfo, string sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }

        public OracleWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }

        protected override SqlMapper.ICustomQueryParameter BuildBigTextParamter(string val)
        {
            return new OracleClobParameter(val);
        }
    }

   
}
