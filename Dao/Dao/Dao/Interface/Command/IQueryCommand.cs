using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1.Dao.Interface.Command
{
    public interface IQueryCommand<T>
    {
        Task<List<T>> ToList();
    }
}
