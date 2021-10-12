using System.Data;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.OracleOperate
{
    internal class OracleInsertable<T, TEntity>  : Insertable<T, TEntity>
    {
        public OracleInsertable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }

        protected override IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            return new OracleWriteableCommand(dbInfo, sql, sqlParameter);
        }
    }
}
