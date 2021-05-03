using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    internal class MsSqlInsertable<T, TEntity> : Insertable<T, TEntity>
    {

        //private string sql = "insert into {0}  values ({1});";


        public MsSqlInsertable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }
    }
}
