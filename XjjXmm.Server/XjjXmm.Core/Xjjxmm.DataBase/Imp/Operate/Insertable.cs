using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Interface;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.Utility;


namespace XjjXmm.DataBase.Imp.Operate
{
    internal abstract class Insertable<T, TEntity> : BaseOperate, IInsertable<T>
    {

        protected readonly TEntity _model;

        public Insertable(DbInfo dbInfo, TEntity model) : base(dbInfo)
        {
            _model = model;
        }

        private StringBuilder Build()
        {
            var columnList = new List<string>();
            var parameterList = new List<string>();

            var type = typeof(T);
           
            var (tableName, properties) = ProviderHelper.GetMetas(type);

         

            foreach (var p in properties)
            {
                if (p.IsIdentity)
                {
                    continue;
                }

                columnList.Add(p.ColumnName);
                parameterList.Add($"{_providerModel.DbInfo.StatementPrefix}{p.Parameter}");


                //if (_model is IEnumerable<>)
                //{

                //}
                //else
                //{
                //    _providerModel.Parameter.Add(p.Parameter, p.PropertyInfo.GetValue(_model));
                //}
                    
                //Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(_model));
            }

            StringBuilder sql = new StringBuilder();

            sql.Append($"insert into {tableName} ({string.Join(",", columnList)}) values ({string.Join(",", parameterList)})");

            return sql;
        }

        /*
        private void buildParamter()
        {
            var type = _model.GetType();


            if (type.IsArray || type.IsGenericType)
            {
                var res = (IEnumerable)_sqlParameter;
                foreach (var re in res)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    BuildSingleParamter(parameter, re);
                    parameters.Add(parameter);
                }
            }
            else
            {
                DynamicParameters parameter = new DynamicParameters();
                BuildSingleParamter(parameter, _sqlParameter);
                parameters.Add(parameter);
            }
        }

        private void BuildSingleParamter(DynamicParameters parameter, object model)
        {
            var type = model.GetType();
            var (tableName, properties) = ProviderHelper.GetMetas(type);

            foreach (var member in properties)
            {
                var name = member.Parameter;
                var val = member.PropertyInfo.GetValue(model);

                //var customAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

                if (member.IsBigText)
                {
                    //byte[] newValue = Encoding.Unicode.GetBytes(val.ToString()); //这里一定要使用 Unicode 字符编码
                    //OracleClob p_content = new OracleClob((OracleConnection)_connection.Value);
                    //p_content.Write(newValue, 0, newValue.Length);

                    //parameter.Add(name, p_content);

                    //OracleClobParameter parameter1 = new OracleClobParameter(val.ToString());

                    parameter.Add(name, BuildBigTextParamter(val.ToString()));


                }
                else
                {

                    //return (name, val);
                    parameter.Add(name, val);
                }


            }
        }
        */


        public async Task<int> Execute()
        {
            //var command = new WriteableCommand(_providerModel.DbInfo, Build().ToString(), _model);
          
            var command = CreateWriteableCommand(_providerModel.DbInfo, Build().ToString(), _model);
            return await command.Execute();
        }

        protected abstract IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter);

    }
}
