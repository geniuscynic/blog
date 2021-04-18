using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DoCare.Extension.DataBase.Interface.Command;
using DoCare.Extension.DataBase.Utility;

namespace DoCare.Extension.DataBase.Imp.Operate
{
    public class SimpleQueryable<T> :BaseOperate, IReaderableCommand<T>
    {
       
        private readonly string _sql;

        public SimpleQueryable(IDbConnection connection, string sql) : this(connection, sql, new Dictionary<string, object>())
        {
           
        }

        public SimpleQueryable(IDbConnection connection, string sql, Dictionary<string, object> sqlParameter) :base(connection, sqlParameter)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            var command = DatabaseFactory.CreateReaderableCommand<T>(Connection, new StringBuilder(_sql), _providerModel.Parameter, Aop);

            return await command.ExecuteQuery();
        }

        public async Task<T> ExecuteFirst()
        {
            var command = DatabaseFactory.CreateReaderableCommand<T>(Connection, new StringBuilder(_sql), _providerModel.Parameter, Aop);

            return await command.ExecuteFirst();
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            var command = DatabaseFactory.CreateReaderableCommand<T>(Connection, new StringBuilder(_sql), _providerModel.Parameter, Aop);

            return await command.ExecuteFirstOrDefault();
        }

        public async Task<T> ExecuteSingle()
        {
            var command = DatabaseFactory.CreateReaderableCommand<T>(Connection, new StringBuilder(_sql), _providerModel.Parameter, Aop);

            return await command.ExecuteSingle();
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            var command = DatabaseFactory.CreateReaderableCommand<T>(Connection, new StringBuilder(_sql), _providerModel.Parameter, Aop);

            return await command.ExecuteSingleOrDefault();
        }

        public async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            var command = DatabaseFactory.CreateReaderableCommand<T>(Connection, new StringBuilder(_sql), _providerModel.Parameter, Aop);

            return await command.ToPageList(pageIndex, pageSize);

        }
    }
}
