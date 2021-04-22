using System.Data;

namespace DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate
{
    public class MySqlSaveable<T, TEntity>  : Saveable<T, TEntity>
    {
       

        public MySqlSaveable(IDbConnection connection, TEntity model): base(connection, model)
        {
           
        }

    }
}
