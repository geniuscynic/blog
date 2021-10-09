using System.Data;
using DoCare.Zkzx.Core.Database.Imp.Command.MySql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
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
