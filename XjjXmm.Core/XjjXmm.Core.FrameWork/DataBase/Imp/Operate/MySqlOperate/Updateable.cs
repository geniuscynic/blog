using System.Data;

namespace DoCare.Extension.DataBase.Imp.Operate.MySqlOperate
{
    public class MySqlUpdateable<T> : Updateable<T>
    {
        public MySqlUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
