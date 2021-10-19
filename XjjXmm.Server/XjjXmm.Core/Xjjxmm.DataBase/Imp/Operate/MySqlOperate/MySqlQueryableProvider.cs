using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.MySqlOperate
{
    internal class MySqlQueryableProvider : QueryableProvider
    {
        public MySqlQueryableProvider(DbInfo dbInfo, string alias) : base(dbInfo, alias)
        {
        }

        protected override IReaderableCommand CreateReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MySqlReaderableCommand(dbInfo, sql, sqlParameter);
        }

        protected override ISqlFuncVisit CreateSqlFunVisit()
        {
            return new MySqlSqlFunc();
        }

        protected override WhereProvider CreateWhereProvider()
        {
            return new MySqlWhereProvider(_providerModel);
        }

        protected override JoinCommand CreateJoinCommand(string alias, ProviderModel providerModel)
        {
            return new MySqlJoinCommand(alias, providerModel);
        }
    }

}
