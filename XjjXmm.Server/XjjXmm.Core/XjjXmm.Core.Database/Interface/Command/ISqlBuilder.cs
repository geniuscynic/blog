using System.Text;

namespace XjjXmm.Core.Database.Interface.Command
{
    interface ISqlBuilder
    {
        StringBuilder Build(bool ignorePrefix = true);
    }
}
