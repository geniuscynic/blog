using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;


namespace DoCare.Zkzx.Core.Database.Imp.Command
{
    internal class JoinCommand : IJoinCommand
    {
        private readonly string _alias;
       // private readonly ProviderModel _providerModel;
        private readonly Dictionary<string, object> _sqlPamater;
        private readonly WhereProvider _joinProvider;
        private readonly WhereProvider _leftJoinProvider;

        public JoinCommand(string alias, ProviderModel providerModel)
        {
            _alias = alias;
            //this._providerModel = _providerModel;
           // _sqlPamater = sqlPamater;
            _joinProvider = new WhereProvider(providerModel);
            _leftJoinProvider = new WhereProvider(providerModel);
        }


        public void Join<T1, T2>(Expression<Func<T1, T2, bool>> predicate)
        {
            _joinProvider.Visit(predicate);
        }

        public void Join<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate)
        {
            _joinProvider.Visit(predicate);
        }

        public void Join<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {
            _joinProvider.Visit(predicate);
        }

        public void LeftJoin<T1, T2>(Expression<Func<T1, T2, bool>> predicate)
        {
            _leftJoinProvider.Visit(predicate);
        }

        public void LeftJoin<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate)
        {
            _leftJoinProvider.Visit(predicate);
        }

        public void LeftJoin<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {
            _leftJoinProvider.Visit(predicate);
        }

        
        public StringBuilder Build<TJoin>()
        {
            var (tableName, _) = ProviderHelper.GetMetas(typeof(TJoin));

            var sql = new StringBuilder();

            if (_joinProvider.whereModel.Sql.Length > 0)
            {
                sql.Append($" join {tableName} {_alias} on ");
                sql.Append(_joinProvider.whereModel.Sql);
            }

            if (_leftJoinProvider.whereModel.Sql.Length > 0)
            {
                sql.Append($" left join {tableName} {_alias} on ");
                sql.Append(_leftJoinProvider.whereModel.Sql);
            }

            
            //sql.Remove(sql.Length - 3, 3);

            //foreach (var keyValuePair in _joinProvider.whereModel.Parameter)
            //{
            //    _sqlPamater[keyValuePair.Key] = keyValuePair.Value;
            //}

            return sql;
        }

       
    }


    //internal class JoinCommand<T1, T2> : IJoinCommand<T1, T2>
    //{
    //    private readonly string _alias;
    //    private readonly Dictionary<string, object> _sqlPamater;
    //    protected readonly WhereProvider _whereProvider = new WhereProvider();

    //    public JoinCommand(string alias, Dictionary<string, object> sqlPamater)
    //    {
    //        _alias = alias;
    //        _sqlPamater = sqlPamater;
    //    }


    //    public void Join(Expression<Func<T1, T2, bool>> predicate)
    //    {
    //        _whereProvider.Visit(predicate);
    //    }

    //    public StringBuilder Build()
    //    {
    //        var (tableName, properties) = ProviderHelper.GetMetas(typeof(T2));

    //        var sql = new StringBuilder();
    //        sql.Append($" join {tableName} {_alias} on ");

    //        sql.Append(_whereProvider.whereModel.Sql);
    //        //sql.Remove(sql.Length - 3, 3);

    //        foreach (var keyValuePair in _whereProvider.whereModel.Parameter)
    //        {
    //            _sqlPamater[keyValuePair.Key] = keyValuePair.Value;
    //        }

    //        return sql;
    //    }
    //}

    //internal class JoinCommand<T1, T2, T3> : JoinCommand<T1, T2>, IJoinCommand<T1, T2, T3>
    //{
    //    public JoinCommand(string alias, Dictionary<string, object> sqlPamater) : base(alias, sqlPamater)
    //    {

    //    }

    //    public void Join(Expression<Func<T1, T2, T3, bool>> predicate)
    //    {
    //        _whereProvider.Visit(predicate);
    //    }
    //}
}
