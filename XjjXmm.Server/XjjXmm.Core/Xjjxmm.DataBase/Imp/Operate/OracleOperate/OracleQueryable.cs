using System.Collections.Generic;
using System.Data;
using System.Text;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.OracleOperate
{
    internal class OracleQueryable<T> : Queryable<T>
    {

        public OracleQueryable(DbInfo dbInfo) : base(dbInfo)
        {
        }

        protected override IReaderableCommand CreateReaderableCommand(DbInfo dbInfo, StringBuilder sql,
            Dictionary<string, object> sqlParameter)
        {
            return new OracleReaderableCommand(dbInfo, sql, sqlParameter);
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new OracleWhereProvider(providerModel);
        }
    }
}
