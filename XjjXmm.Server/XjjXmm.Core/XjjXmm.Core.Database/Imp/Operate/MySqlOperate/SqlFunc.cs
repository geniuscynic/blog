﻿using System;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{

    internal class MySqlSqlFunc : SqlFuncVisit
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
