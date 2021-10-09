using System.Threading.Tasks;

namespace DoCare.Zkzx.Core.Database.Interface.Command
{
    public interface IWriteableCommand   {
        Task<int> Execute();

       
    }
}
