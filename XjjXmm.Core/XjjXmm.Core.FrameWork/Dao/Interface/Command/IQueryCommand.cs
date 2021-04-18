using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Extension.Dao.Interface.Command
{
    public interface IQueryCommand<T>
    {
        Task<IEnumerable<T>> ExecuteQuery();

        Task<T> ExecuteFirst();

        Task<T> ExecuteFirstOrDefault();

        Task<T> ExecuteSingle();

        Task<T> ExecuteSingleOrDefault();
    }
}
