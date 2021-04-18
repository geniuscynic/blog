using System.Data;

namespace DoCare.Extension.DataBase.Imp.Operate.SqlOperate
{
    public class SqlUpdateable<T> : Updateable<T>
    {
        public SqlUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
