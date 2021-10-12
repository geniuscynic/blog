using System.Data;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.MySqlOperate
{
    internal class MySqlDeleteable<T> : Deleteable<T>
    {
        public MySqlDeleteable(DbInfo dbInfo) : base(dbInfo)
        {
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new MySqlWhereProvider(providerModel);
        }

        protected override IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            return new MysqlWriteableCommand(dbInfo, sql, sqlParameter);
        }
    }
}
