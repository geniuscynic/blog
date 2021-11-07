using System;
using System.Collections.Generic;

namespace XjjXmm.DataBase.Interface.Operate
{


    public interface ISqlFuncVisit
    {
        string IsNull(string p1);

        //string IsNull(DateTime? p1);

        string Like(string p1, string p2);

        string Contain(IEnumerable<string> p1, object p2);
        
        string Contain(IEnumerable<long> p1, long p2);
        
        string FormatDate(string date, string format);


        string CovertDateToString(string date, string format);

        string Lower(string p1);

        string Upper(string p1);
    }


}
