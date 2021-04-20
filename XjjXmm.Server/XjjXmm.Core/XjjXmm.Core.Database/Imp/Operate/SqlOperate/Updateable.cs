using System.Data;

namespace XjjXmm.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlUpdateable<T> : Updateable<T>
    {
        public SqlUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
