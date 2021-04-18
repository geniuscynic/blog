using System.Data;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlDeleteable<T> : Deleteable<T>
    {
        public MySqlDeleteable(IDbConnection connection) : base(connection)
        {


        }
    }
}
