using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace XjjXmm.DataBase.Imp.Command
{
    internal abstract class WriteableCommand : IWriteableCommand
    {
        //private readonly DbInfo _dbInfo;
        private readonly Lazy<IDbConnection> _connection;
        private readonly string _sql;
        private readonly object _sqlParameter;
        private readonly Aop _aop;


        public WriteableCommand(DbInfo dbInfo, string sql, Dictionary<string, object> sqlParameter) : this(dbInfo, sql, (object)sqlParameter)
        {

        }

        public WriteableCommand(DbInfo dbInfo, string sql, object sqlParameter)
        {
            _aop = dbInfo.Aop;
            _connection = dbInfo.Connection;

            _sql = sql;
            _sqlParameter = sqlParameter;

        }

        protected abstract SqlMapper.ICustomQueryParameter BuildBigTextParamter(string val);
        private List<DynamicParameters> BuildParameters()
        {
            var parameters = new List<DynamicParameters>();

           

            if (_sqlParameter is Dictionary<string, object>)
            {
                var sqlParas = (Dictionary<string, object>) _sqlParameter;

                DynamicParameters parameter = new DynamicParameters();
                foreach (var para in sqlParas)
                {
                    var name = para.Key;
                    var val = para.Value;


                    parameter.Add(name, val);
                }

                parameters.Add(parameter);
            }
            else
            {
                var type = _sqlParameter.GetType();

               
                if (type.IsArray || type.IsGenericType)
                {
                    var res = (IEnumerable) _sqlParameter;
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


            return parameters;
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

        public async Task<int> Execute()
        {
            //var sql = Build().ToString();

            try
            {
                var parameter = BuildParameters();

                _aop?.OnExecuting?.Invoke(_sql, _sqlParameter);

                var result = await _connection.Value.ExecuteAsync(_sql, parameter);

                _aop?.OnExecuted?.Invoke(_sql, _sqlParameter);

                return result;
            }
            catch (Exception ex)
            {
                _aop?.OnError?.Invoke(_sql, _sqlParameter, ex);
                throw;
            }
        }
    }
}
