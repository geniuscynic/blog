using System.Data;
using XjjXmm.DataBase.Imp.Command.MsSql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.SqlOperate
{
    internal class SqlSaveable<T, TEntity>  : Saveable<T, TEntity>
    {
        public SqlSaveable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }

        protected override IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            return new MsSqlWriteableCommand(dbInfo, sql, sqlParameter);
        }
    }
}
