using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate
{

    internal class MsSqlSqlFunc : SqlFuncVisit
    {
        public override string IsNull(string p1)
        {
            return $"nvl2({p1}， 1, 0)";
        }

       
    }
}
