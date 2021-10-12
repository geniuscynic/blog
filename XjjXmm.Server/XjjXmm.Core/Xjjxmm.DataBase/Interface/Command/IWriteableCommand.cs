using System.Threading.Tasks;

namespace XjjXmm.DataBase.Interface.Command
{
    public interface IWriteableCommand   {
        Task<int> Execute();

       
    }
}
