using System.Collections.Generic;
using System.Data;
using System.Text;
using XjjXmm.DataBase.Imp.Command.MsSql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.SqlOperate
{
    internal class MsSqlQueryable<T> : Queryable<T>
    {
        public MsSqlQueryable(DbInfo dbInfo) : base(dbInfo)
        {
        }


        protected override IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql,
            Dictionary<string, object> sqlParameter)
        {
            return new MsSqlReaderableCommand<TResult>(dbInfo, sql, sqlParameter);
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new MsSqlWhereProvider(providerModel);
        }
    }
}
