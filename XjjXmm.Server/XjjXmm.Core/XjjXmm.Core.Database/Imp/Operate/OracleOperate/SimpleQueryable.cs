using System.Collections.Generic;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command.MySql;
using DoCare.Zkzx.Core.Database.Imp.Command.Oracle;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate
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
