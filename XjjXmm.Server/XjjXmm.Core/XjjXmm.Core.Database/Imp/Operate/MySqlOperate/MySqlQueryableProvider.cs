using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database.Imp.Command;
using DoCare.Zkzx.Core.Database.Imp.Command.MySql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    internal class MySqlQueryableProvider : QueryableProvider
    {
        public MySqlQueryableProvider(DbInfo dbInfo, string alias) : base(dbInfo, alias)
        {
        }

        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MySqlReaderableCommand<TResult>(dbInfo, sql, sqlParameter);
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
