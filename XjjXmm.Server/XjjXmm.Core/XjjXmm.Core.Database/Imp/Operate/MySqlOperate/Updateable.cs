using System.Data;

namespace XjjXmm.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlUpdateable<T> : Updateable<T>
    {
        public MySqlUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
