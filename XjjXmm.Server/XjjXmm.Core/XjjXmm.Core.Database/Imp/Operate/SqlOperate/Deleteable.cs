using System.Data;
using DoCare.Zkzx.Core.Database.Imp.Command.MsSql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
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
