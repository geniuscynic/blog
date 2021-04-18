using System.Data;

namespace DoCare.Extension.DataBase.Imp.Operate.SqlOperate
{
    public class SqlDeleteable<T> : Deleteable<T>
    {
        public SqlDeleteable(IDbConnection connection) : base(connection)
        {


        }
    }
}
