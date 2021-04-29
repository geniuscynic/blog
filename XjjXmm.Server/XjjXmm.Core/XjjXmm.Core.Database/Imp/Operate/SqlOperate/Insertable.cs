using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlInsertable<T, TEntity> : Insertable<T, TEntity>
    {

        //private string sql = "insert into {0}  values ({1});";


        public SqlInsertable(DbInfo dbClientParamter, TEntity model) : base(dbClientParamter, model)
        {
        }
    }
}
