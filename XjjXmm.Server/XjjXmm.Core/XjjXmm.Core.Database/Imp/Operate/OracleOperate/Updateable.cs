using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate
{
    public class OracleUpdateable<T> : Updateable<T>
    {
        public OracleUpdateable(DbInfo info) : base(info)
        {
        }
    }
}
