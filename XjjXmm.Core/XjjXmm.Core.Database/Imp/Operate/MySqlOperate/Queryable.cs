using System.Collections.Generic;
using System.Data;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command.MySql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
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
