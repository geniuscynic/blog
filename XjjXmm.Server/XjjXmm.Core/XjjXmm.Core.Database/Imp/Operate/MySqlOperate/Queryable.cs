using System.Collections.Generic;
using System.Data;
using System.Text;
using XjjXmm.Core.Database.Imp.Command.MySql;
using XjjXmm.Core.Database.Interface.Command;
using XjjXmm.Core.Database.Utility;

namespace XjjXmm.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlQueryable<T> : Queryable<T>
    {
        public MySqlQueryable(IDbConnection connection):base(connection)
        {
            

        }


        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(IDbConnection connection, StringBuilder sql, Dictionary<string, object> sqlParameter,
            Aop aop)
        {
            return new MySqlReaderableCommand<TResult>(connection, sql, sqlParameter, aop);
        }
    }
}
