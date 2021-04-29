using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate
{
    public class OracleSqlSaveable<T, TEntity>  : Saveable<T, TEntity>
    {
        public OracleSqlSaveable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }
    }
}
