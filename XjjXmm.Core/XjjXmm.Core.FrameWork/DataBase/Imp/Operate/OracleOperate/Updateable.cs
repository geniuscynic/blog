using System.Data;

namespace DoCare.Extension.DataBase.Imp.Operate.OracleOperate
{
    public class OracleUpdateable<T> : Updateable<T>
    {
        public OracleUpdateable(IDbConnection connection) : base(connection)
        {


        }
    }
}
