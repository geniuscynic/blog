using System.Data;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.MySqlOperate
{
    internal class MySqlUpdateable<T> : Updateable<T>
    {
        public MySqlUpdateable(DbInfo info) : base(info)
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
