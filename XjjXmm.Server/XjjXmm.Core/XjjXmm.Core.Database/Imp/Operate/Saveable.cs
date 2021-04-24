using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Imp.Command;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;


namespace DoCare.Zkzx.Core.Database.Imp.Operate
{
    public class Saveable<T, TEntity>  : BaseOperate, ISaveable<T>
    {
       
        protected readonly TEntity _model;

        private readonly SelectProvider _selectProvider = new SelectProvider();
        private readonly SelectProvider _ignoreProvider = new SelectProvider();

        

        public Saveable(IDbConnection connection, TEntity model): base(connection)
        {
           
            _model = model;

        }

        public ISaveable<T> UpdateColumns<TResult>(Expression<Func<T,TResult>> predicate)
        {
            
            _selectProvider.Visit(predicate);

            return this;
        }

        public ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate)
        {
            _ignoreProvider.Visit(predicate);

            return this;
        }


        private StringBuilder Build()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, properties) = ProviderHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");

            IEnumerable<Member> existProperty;

            if (_selectProvider.SelectFields.Any())
            {
                existProperty = properties.Where(p =>  _selectProvider.SelectFields.All(t => t.ColumnName != p.ColumnName));
            }
            else
            {
                existProperty = properties.Where(p => !p.IsPrimaryKey && !p.IgnoreSave && _ignoreProvider.SelectFields.All(t => t.ColumnName != p.ColumnName));
            }

            foreach (var p in existProperty)
            {
                sql.Append($" {p.ColumnName} = {_providerModel.DataParamterPrefix}{p.Parameter},");
                _providerModel.Parameter.Add(p.Parameter, p.PropertyInfo.GetValue(_model));
            }

            sql.Remove(sql.Length - 1, 1);

            sql.Append(" where ");

            foreach (var member in properties.Where(t => t.IsPrimaryKey))
            {
                sql.Append($" {member.ColumnName} = {_providerModel.DataParamterPrefix}{member.Parameter} and");
                _providerModel.Parameter.Add(member.Parameter, member.PropertyInfo.GetValue(_model));
            }

            sql.Remove(sql.Length - 3, 3);

            return sql;
        }

        public async Task<int> Execute()
        {
            var command = new WriteableCommand(Connection, Build().ToString(), _providerModel.Parameter, Aop);

            return await command.Execute();
        }
    }
}
