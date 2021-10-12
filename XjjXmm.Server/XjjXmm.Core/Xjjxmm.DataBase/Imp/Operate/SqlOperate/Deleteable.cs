using System.Data;
using XjjXmm.DataBase.Imp.Command.MsSql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.SqlOperate
{
    internal class MsSqlDeleteable<T> : Deleteable<T>
    {
        public MsSqlDeleteable(DbInfo dbInfo) : base(dbInfo)
        {
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new MsSqlWhereProvider(providerModel);
        }

        protected override IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            return new MsSqlWriteableCommand(dbInfo, sql, sqlParameter);
        }
    }
}
