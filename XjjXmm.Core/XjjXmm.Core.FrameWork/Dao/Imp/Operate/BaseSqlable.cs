using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.Dao.Common;
using DoCare.Extension.Dao.Interface.Command;
using DoCare.Extension.Dao.Interface.Operate;

namespace DoCare.Extension.Dao.Imp.Operate
{
    public class BaseSqlable<T>  
    {
        protected readonly IDbConnection _connection;
        
        protected string paramterPrefix;

        protected Dictionary<string, object> _sqlPamater = new Dictionary<string, object>();

        public Aop Aop { get; set; }



        public BaseSqlable(IDbConnection connection)
        {
            _connection = connection;
            
            paramterPrefix = DatabaseFactory.GetStatementPrefix(connection);
        }

       
    }
}
