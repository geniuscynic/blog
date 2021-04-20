using System.Data;

namespace XjjXmm.Core.Database.Imp.Operate.OracleOperate
{
    public class OracleUpdateable<T> : Updateable<T>
    {
        public OracleUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
