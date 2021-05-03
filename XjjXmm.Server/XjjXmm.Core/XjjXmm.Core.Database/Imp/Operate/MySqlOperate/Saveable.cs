using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    internal class MySqlSaveable<T, TEntity>  : Saveable<T, TEntity>
    {
        public MySqlSaveable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }
    }
}
