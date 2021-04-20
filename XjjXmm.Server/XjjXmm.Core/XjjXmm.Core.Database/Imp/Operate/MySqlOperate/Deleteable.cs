using System.Data;

namespace XjjXmm.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlDeleteable<T> : Deleteable<T>
    {
        public MySqlDeleteable(IDbConnection connection) : base(connection)
        {


        }
    }
}
