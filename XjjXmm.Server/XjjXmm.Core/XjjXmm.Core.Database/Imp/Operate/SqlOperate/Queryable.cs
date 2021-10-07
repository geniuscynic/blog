using System.Collections.Generic;
using System.Data;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command.MsSql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
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
