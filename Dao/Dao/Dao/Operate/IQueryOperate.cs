using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Dao.Operate
{
    public interface IQueryOperate
    {
       
        Task<int> Execute();
    }
}
