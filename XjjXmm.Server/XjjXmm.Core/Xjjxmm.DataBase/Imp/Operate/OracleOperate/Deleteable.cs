using System.Data;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.OracleOperate
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
