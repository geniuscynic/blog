using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlUpdateable<T> : Updateable<T>
    {
        public MySqlUpdateable(DbInfo info) : base(info)
        {
        }
    }
}
