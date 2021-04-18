using System.Text;

namespace DoCare.Extension.DataBase.Interface.Command
{
    interface ISqlBuilder
    {
        StringBuilder Build(bool ignorePrefix = true);
    }
}
