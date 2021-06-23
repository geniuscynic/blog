using System.Collections.Generic;

namespace DoCare.Zkzx.Core.Database.Interface.Operate
{


    public interface ISqlFuncVisit
    {
        string IsNull(string p1);

        string Like(string p1, string p2);

        string Contain(List<string> p1, string p2);


        string FormatDate(string date, string format);


        string CovertDateToString(string date, string format);
    }


}
