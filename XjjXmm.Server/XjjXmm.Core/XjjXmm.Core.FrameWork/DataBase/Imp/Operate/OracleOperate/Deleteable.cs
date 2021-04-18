using System.Data;

namespace DoCare.Extension.DataBase.Imp.Operate.OracleOperate
{
    public class OracleDeleteable<T> : Deleteable<T>
    {
        public OracleDeleteable(IDbConnection connection) : base(connection)
        {


        }
    }
}
