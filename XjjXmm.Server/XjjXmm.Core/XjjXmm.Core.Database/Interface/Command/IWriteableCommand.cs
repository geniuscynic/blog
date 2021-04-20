using System.Threading.Tasks;

namespace XjjXmm.Core.Database.Interface.Command
{
    public interface IWriteableCommand   {
        Task<int> Execute();
    }
}
