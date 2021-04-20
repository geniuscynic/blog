using System.Data;

namespace XjjXmm.Core.Database.Imp.Operate.SqlOperate
{
    public class SqlDeleteable<T> : Deleteable<T>
    {
        public SqlDeleteable(IDbConnection connection) : base(connection)
        {


        }
    }
}
