using System.Data;

namespace DoCare.Extension.Dao.Imp.Operate.SqlOperate
{
    public class OracleQueryable<T> : Queryable<T>
    {
        public OracleQueryable(IDbConnection connection):base(connection)
        {
            

        }

      
    }
}
