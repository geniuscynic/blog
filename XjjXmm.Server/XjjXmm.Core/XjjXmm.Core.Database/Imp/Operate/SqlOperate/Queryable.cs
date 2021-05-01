using System.Collections.Generic;
using System.Data;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command.MsSql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlQueryable<T> : Queryable<T>
    {
        public SqlQueryable(DbInfo dbInfo) : base(dbInfo)
        {
        }


        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql,
            Dictionary<string, object> sqlParameter)
        {
            return new MsSqlReaderableCommand<TResult>(dbInfo, sql, sqlParameter);
        }

       
    }
}
