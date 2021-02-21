using System.Threading.Tasks;

namespace ConsoleApp1.Dao.Interface.Command
{
    public interface IExecuteCommand
    {
        Task<int> Execute();
    }
}
