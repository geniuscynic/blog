using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    internal class MySqlInsertable<T, TEntity>  : Insertable<T, TEntity>
    {
       
        //private string sql = "insert into {0}  values ({1});";


        public MySqlInsertable(DbInfo dbInfo, TEntity model) : base(dbInfo, model)
        {
        }
    }
}
