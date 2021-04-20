using System.Collections.Generic;
using System.Data;
using System.Text;
using XjjXmm.Core.Database.Imp.Command.MsSql;
using XjjXmm.Core.Database.Interface.Command;
using XjjXmm.Core.Database.Utility;

namespace XjjXmm.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlQueryable<T> : Queryable<T>
    {
        public SqlQueryable(IDbConnection connection):base(connection)
        {
            

        }


        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(IDbConnection connection, StringBuilder sql,
            Dictionary<string, object> sqlParameter, Aop aop)
        {
            return new MsSqlReaderableCommand<TResult>(connection, sql, sqlParameter, aop);
        }
    }
}
