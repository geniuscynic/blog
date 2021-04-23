using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate
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
            if (pageIndex < 1)
            {
                throw new Exception("pageIndex 不能小于1页");
            }

            if (pageSize < 1)
            {
                throw new Exception("pageSize 不能小于1条");
            }

            var command = DatabaseFactory.CreateReaderableCommand<T>(Connection, new StringBuilder(_sql), _providerModel.Parameter, Aop);

            return await command.ToPageList(pageIndex, pageSize);

        }
    }
}
