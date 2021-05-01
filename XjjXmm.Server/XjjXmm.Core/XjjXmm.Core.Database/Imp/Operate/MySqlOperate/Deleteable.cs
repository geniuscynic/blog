using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlDeleteable<T> : Deleteable<T>
    {
        public MySqlDeleteable(DbInfo dbInfo) : base(dbInfo)
        {
        }
    }
}
