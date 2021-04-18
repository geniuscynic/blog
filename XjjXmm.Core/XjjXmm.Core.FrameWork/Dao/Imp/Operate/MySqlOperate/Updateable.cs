using System.Data;

namespace DoCare.Extension.Dao.Imp.Operate.MySqlOperate
{
    public class MySqlUpdateable<T> : Updateable<T>
    {
        public MySqlUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
