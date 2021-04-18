using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.DataBase.Imp.Command;
using DoCare.Extension.DataBase.Interface;
using DoCare.Extension.DataBase.Interface.Command;
using DoCare.Extension.DataBase.Interface.Operate;
using DoCare.Extension.DataBase.Utility;


namespace DoCare.Extension.DataBase.Imp.Operate
{
    public class Insertable<T, TEntity> : BaseOperate, IInsertable<T>
    {

        protected readonly TEntity _model;

        public Insertable(IDbConnection connection, TEntity model) : base(connection)
        {
            _model = model;
        }

        private StringBuilder Build()
        {
            var columnList = new List<string>();
            var parameterList = new List<string>();

            var type = typeof(T);
           
            var (tableName, properties) = Utility.ProviderHelper.GetMetas(type);

            foreach (var p in properties)
            {
                if (p.IsIdentity)
                {
                    continue;
                }

                columnList.Add(p.ColumnName);
                parameterList.Add($"{_providerModel.DataParamterPrefix}{p.Parameter}");
                _providerModel.Parameter.Add(p.Parameter, p.PropertyInfo.GetValue(_model));
                //Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(_model));
            }

            StringBuilder sql = new StringBuilder();

            sql.Append($"insert into {tableName} ({string.Join(",", columnList)}) values ({string.Join(",", parameterList)})");

            return sql;
        }


        public async Task<int> Execute()
        {
            var command = new WriteableCommand(Connection, Build().ToString(), _providerModel.Parameter, Aop);
          
            return await command.Execute();
        }

      
    }
}
