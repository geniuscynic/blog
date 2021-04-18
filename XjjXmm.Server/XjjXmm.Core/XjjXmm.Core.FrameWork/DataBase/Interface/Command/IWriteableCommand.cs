using System.Threading.Tasks;

namespace DoCare.Extension.DataBase.Interface.Command
{
    public interface IWriteableCommand   {
        Task<int> Execute();
    }
}
