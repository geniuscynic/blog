using System.Collections.Generic;
using System.Text;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.MySqlOperate
{
    internal class MySqlSimpleQueryable<T> :SimpleQueryable<T>
    {
        public MySqlSimpleQueryable(DbInfo info, string sql) : base(info, sql)
        {
        }

        public MySqlSimpleQueryable(DbInfo info, string sql, Dictionary<string, object> sqlParameter) : base(info, sql, sqlParameter)
        {
        }

        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MySqlReaderableCommand<TResult>(dbInfo,sql, sqlParameter);
        }
    }
}
