using System.Collections.Generic;
using System.Text;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.OracleOperate
{
    internal class OracleQueryableProvider : QueryableProvider
    {
        public OracleQueryableProvider(DbInfo dbInfo, string alias) : base(dbInfo, alias)
        {
        }

        protected override IReaderableCommand CreateReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new OracleReaderableCommand(dbInfo, sql, sqlParameter);
        }

        protected override ISqlFuncVisit CreateSqlFunVisit()
        {
            return new OracleSqlFunc();
        }

        protected override WhereProvider CreateWhereProvider()
        {
            return new OracleWhereProvider(_providerModel);
        }

        protected override JoinCommand CreateJoinCommand(string alias, ProviderModel providerModel)
        {
            return new OracleSqlJoinCommand(alias, providerModel);
        }
    }

}
