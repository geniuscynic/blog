﻿using System;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate.OracleOperate
{

    internal class OracleSqlFunc : SqlFuncVisit
    {
        public override string IsNull(string p1)
        {
            return $"nvl2({p1}， 1, 0)";
        }

        //public override string IsNull(DateTime? p1)
        //{
        //    return $"nvl2({p1}， 1, 0)";
        //}
    }
}
