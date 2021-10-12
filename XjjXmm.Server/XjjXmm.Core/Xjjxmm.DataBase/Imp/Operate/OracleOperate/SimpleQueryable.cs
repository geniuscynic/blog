using System.Collections.Generic;
using System.Text;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.OracleOperate
{
    internal class OracleSimpleQueryable<T> :SimpleQueryable<T>
    {
        public OracleSimpleQueryable(DbInfo info, string sql) : base(info, sql)
        {
        }

        public OracleSimpleQueryable(DbInfo info, string sql, Dictionary<string, object> sqlParameter) : base(info, sql, sqlParameter)
        {
        }

        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new OracleReaderableCommand<TResult>(dbInfo,sql, sqlParameter);
        }
    }
}
