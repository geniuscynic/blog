using System.Data;
using DoCare.Extension.Dao.Interface.Command;
using DoCare.Extension.Dao.Interface.Operate;

namespace DoCare.Extension.Dao.Imp.Operate.OracleOperate
{
    public class OracleInsertable<T, TEntity>  : Insertable<T, TEntity>
    {
       
       
        public OracleInsertable(IDbConnection connection, TEntity model) : base(connection, model)
        {
            
        }

       
    }
}
