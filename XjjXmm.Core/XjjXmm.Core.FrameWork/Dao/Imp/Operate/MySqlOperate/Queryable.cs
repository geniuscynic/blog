using System.Data;

namespace DoCare.Extension.Dao.Imp.Operate.MySqlOperate
{
    public class MySqlQueryable<T> : Queryable<T>
    {
        public MySqlQueryable(IDbConnection connection):base(connection)
        {
            

        }

      
    }
}
