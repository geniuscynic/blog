using System.Data;
using DoCare.Zkzx.Core.Database.Imp.Command.Oracle;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate
{
    internal class OracleDeleteable<T> : Deleteable<T>
    {
        public OracleDeleteable(DbInfo dbInfo) : base(dbInfo)
        {
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new OracleWhereProvider(providerModel);
        }

        protected override IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            return new OracleWriteableCommand(dbInfo, sql, sqlParameter);
        }
    }
}
