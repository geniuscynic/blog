using System.Threading.Tasks;

namespace DoCare.Extension.Dao.Interface.Command
{
    public interface IExecuteCommand
    {
        Task<int> Execute();
    }
}
