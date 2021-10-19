using System.Collections.Generic;
using System.Text;
using XjjXmm.DataBase.Imp.Command.MsSql;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.SqlOperate
{
    internal class MsSqlSimpleQueryable<T> :SimpleQueryable<T>
    {
        public MsSqlSimpleQueryable(DbInfo info, string sql) : base(info, sql)
        {
        }

        public MsSqlSimpleQueryable(DbInfo info, string sql, Dictionary<string, object> sqlParameter) : base(info, sql, sqlParameter)
        {
        }

        protected override IReaderableCommand CreateReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MsSqlReaderableCommand(dbInfo,sql, sqlParameter);
        }
    }
}
