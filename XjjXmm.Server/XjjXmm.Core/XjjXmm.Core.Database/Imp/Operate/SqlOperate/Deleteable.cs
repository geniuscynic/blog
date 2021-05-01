using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlDeleteable<T> : Deleteable<T>
    {
        public SqlDeleteable(DbInfo dbInfo) : base(dbInfo)
        {
        }
    }
}
