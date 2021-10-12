using System.Collections.Generic;
using System.Data;
using System.Text;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.MySqlOperate
{
    internal class MySqlQueryable<T> : Queryable<T>
    {
        public MySqlQueryable(DbInfo dbInfo) : base(dbInfo)
        {
        }


        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo info, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return new MySqlReaderableCommand<TResult>(info, sql, sqlParameter);
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new MySqlWhereProvider(providerModel);
        }
    }
}
