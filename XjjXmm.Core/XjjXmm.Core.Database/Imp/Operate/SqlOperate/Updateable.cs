using System.Data;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlUpdateable<T> : Updateable<T>
    {
        public SqlUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
