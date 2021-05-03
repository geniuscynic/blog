using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate
{
    public abstract class SimpleQueryable<T> :BaseOperate, IReaderableCommand<T>
    {
       
        private readonly string _sql;

        public SimpleQueryable(DbInfo info, string sql) : this(info, sql, new Dictionary<string, object>())
        {
           
        }

        public SimpleQueryable(DbInfo info, string sql, Dictionary<string, object> sqlParameter) :base(info, sqlParameter)
        {
            _sql = sql;
        }

        protected abstract IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter);


        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            //var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            return await command.ExecuteQuery();
        }

        public async Task<T> ExecuteFirst()
        {
            // var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            return await command.ExecuteFirst();
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            //var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            return await command.ExecuteFirstOrDefault();
        }

        public async Task<T> ExecuteSingle()
        {
            //var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);


            return await command.ExecuteSingle();
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            // var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

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

            //var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, new StringBuilder(_sql), _providerModel.Parameter);

            return await command.ToPageList(pageIndex, pageSize);

        }
    }
}
