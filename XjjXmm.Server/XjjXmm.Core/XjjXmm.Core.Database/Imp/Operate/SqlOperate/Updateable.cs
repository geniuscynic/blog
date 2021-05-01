using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlUpdateable<T> : Updateable<T>
    {
        public SqlUpdateable(DbInfo info) : base(info)
        {
        }
    }
}
