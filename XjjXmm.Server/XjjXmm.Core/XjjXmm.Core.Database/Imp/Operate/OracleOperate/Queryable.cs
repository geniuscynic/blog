using System.Collections.Generic;
using System.Data;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command.Oracle;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate
{
    public class OracleQueryable<T> : Queryable<T>
    {
        public OracleQueryable(IDbConnection connection):base(connection)
        {
            

        }


        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(IDbConnection connection, StringBuilder sql,
            Dictionary<string, object> sqlParameter, Aop aop)
        {
            return new OracleReaderableCommand<TResult>(connection, sql, sqlParameter, aop);
        }
    }
}
