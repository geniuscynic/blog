using System.Text;

namespace XjjXmm.DataBase.Interface.Command
{
    interface ISqlBuilder
    {
        StringBuilder Build(bool ignorePrefix = true);
    }
}
