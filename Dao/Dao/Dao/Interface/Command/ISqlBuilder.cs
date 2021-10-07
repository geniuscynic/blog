using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Dao.Interface.Command
{
    interface ISqlBuilder
    {
        string Build(bool ignorePrefix = true);
    }
}
