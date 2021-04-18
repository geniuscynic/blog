using System.Data;

namespace DoCare.Extension.Dao.Imp.Operate.SqlOperate
{
    public class SqlInsertable<T, TEntity>  : Insertable<T, TEntity>
    {
       
        //private string sql = "insert into {0}  values ({1});";
        public SqlInsertable(IDbConnection connection, TEntity model):base(connection,model)
        {
            
        }

    }
}
