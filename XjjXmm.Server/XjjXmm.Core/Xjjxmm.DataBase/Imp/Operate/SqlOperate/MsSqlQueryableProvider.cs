using System.Collections.Generic;
using System.Text;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Imp.Command.MsSql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.SqlOperate
{
    internal class MsSqlQueryableProvider : QueryableProvider
    {
        public MsSqlQueryableProvider(DbInfo dbInfo, string alias) : base(dbInfo, alias)
        {
        }

        protected override IReaderableCommand CreateReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MsSqlReaderableCommand(dbInfo, sql, sqlParameter);
        }

        protected override ISqlFuncVisit CreateSqlFunVisit()
        {
            return new MsSqlSqlFunc();
        }

        protected override WhereProvider CreateWhereProvider()
        {
            return new MsSqlWhereProvider(_providerModel);
        }

        protected override JoinCommand CreateJoinCommand(string alias, ProviderModel providerModel)
        {
            return new MsSqlJoinCommand(alias, providerModel);
        }
    }

}
