using System.Text;

namespace DoCare.Zkzx.Core.Database.Interface.Command
{
    interface ISqlBuilder
    {
        StringBuilder Build(bool ignorePrefix = true);
    }
}
