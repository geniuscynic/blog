using System.Data;

namespace XjjXmm.Core.Database.Imp.Operate.OracleOperate
{
    public class OracleDeleteable<T> : Deleteable<T>
    {
        public OracleDeleteable(IDbConnection connection) : base(connection)
        {


        }
    }
}
