using System.Data;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.MySqlOperate
{
    internal class MySqlSaveable<T, TEntity>  : Saveable<T, TEntity>
    {
        public MySqlSaveable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }

        protected override IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            return new MysqlWriteableCommand(dbInfo, sql, sqlParameter);
        }
    }
}
