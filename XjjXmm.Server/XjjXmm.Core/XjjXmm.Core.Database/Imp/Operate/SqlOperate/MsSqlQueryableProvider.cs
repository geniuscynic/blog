using System.Collections.Generic;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command;
using DoCare.Zkzx.Core.Database.Imp.Command.MsSql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    internal class MsSqlQueryableProvider : QueryableProvider
    {
        public MsSqlQueryableProvider(DbInfo dbInfo, string alias) : base(dbInfo, alias)
        {
        }

        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MsSqlReaderableCommand<TResult>(dbInfo, sql, sqlParameter);
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
