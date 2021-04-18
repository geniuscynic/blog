using System.Data;

namespace DoCare.Extension.DataBase.Imp.Operate.OracleOperate
{
    public class OracleQueryable<T> : Queryable<T>
    {
        public OracleQueryable(IDbConnection connection):base(connection)
        {
            

        }

      
    }
}
