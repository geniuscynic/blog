using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlSaveable<T, TEntity>  : Saveable<T, TEntity>
    {
        public SqlSaveable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }
    }
}
