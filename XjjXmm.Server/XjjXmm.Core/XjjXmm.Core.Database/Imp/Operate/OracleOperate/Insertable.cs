using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate
{
    public class OracleInsertable<T, TEntity>  : Insertable<T, TEntity>
    {
        public OracleInsertable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }
    }
}
