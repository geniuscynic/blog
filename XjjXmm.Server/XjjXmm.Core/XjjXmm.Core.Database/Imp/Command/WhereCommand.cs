using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;


namespace DoCare.Zkzx.Core.Database.Imp.Command
{
    internal class WhereCommand : IWhereCommand
    {
        private readonly ProviderModel _providerModel;
        //private readonly Dictionary<string, object> _sqlPamater;
        protected readonly WhereProvider _whereProvider;

        private protected readonly StringBuilder _whereCause = new StringBuilder();

        public string prefix = "";

        public WhereCommand(ProviderModel providerModel, WhereProvider whereProvider)
        {
            _providerModel = providerModel;
            //_whereProvider = new WhereProvider(providerModel);
            _whereProvider = whereProvider;

            // _sqlPamater = sqlPamater;
        }

        private void VisitPredicate(Expression predicate)
        {
            _whereProvider.Visit(predicate);

            _whereProvider.whereModel.Sql.Append(" and");

            prefix = _whereProvider.whereModel.Prefix;
        }
        public void Where<T1>(Expression<Func<T1, bool>> predicate)
        {
            VisitPredicate(predicate);
        }

        public void Where<T1, T2>(Expression<Func<T1, T2, bool>> predicate)
        {
            VisitPredicate(predicate);
        }

        public void Where<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate)
        {
            VisitPredicate(predicate);
        }

        public void Where<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {
            VisitPredicate(predicate);
        }


        public void Where(string whereExpression)
        {
            _whereCause.Append($" ({whereExpression}) and");
        }

        public void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate)
        {
            _whereCause.Append($" ({whereExpression}) and");

            var provider = new SelectProvider();
            provider.Visit(predicate);

            //var dic = (IDictionary<string, object>)_dynamicModel;

            var model = predicate.Compile().Invoke();
            var types = model.GetType();


            provider.SelectFields.ForEach(t =>
            {
                var values = types.GetProperty(t.Parameter)?.GetValue(model);

                _providerModel.Parameter[t.Parameter] = values;
            });
        }

        public StringBuilder Build(bool ignorePrefix = true)
        {
            var sql = new StringBuilder();

            if (_whereCause.Length == 0 && _whereProvider.whereModel.Sql.Length == 0)
            {
                return sql;
            }

            sql.Append(" where ");

            sql.Append(_whereCause);

            sql.Append(_whereProvider.whereModel.Sql);
            sql.Remove(sql.Length - 3, 3);

            //foreach (var keyValuePair in _whereProvider.whereModel.Parameter)
            //{
            //    _sqlPamater[keyValuePair.Key] = keyValuePair.Value;
            //}

            return ignorePrefix ? sql.Replace($"{prefix}.", "") : sql;
        }
    }

    //internal class WhereCommand<T> : IWhereCommand<T>
    //{
    //    private readonly Dictionary<string, object> _sqlPamater;
    //    protected readonly WhereProvider _whereProvider;// = new WhereProvider();

    //    private readonly StringBuilder _whereCause = new StringBuilder();

    //    public string prefix = "";

    //    public WhereCommand(Dictionary<string, object> sqlPamater)
    //    {
    //        _sqlPamater = sqlPamater;
    //    }

    //    public void Where(Expression<Func<T, bool>> predicate)
    //    {
    //        _whereProvider.Visit(predicate);

    //        _whereProvider.whereModel.Sql.Append(" and");

    //        prefix = _whereProvider.whereModel.Prefix;
    //    }

    //    public void Where(string whereExpression)
    //    {
    //        _whereCause.Append($" ({whereExpression}) and");
    //    }

    //    public void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate)
    //    {
    //        _whereCause.Append($" ({whereExpression}) and");

    //        var provider = new SelectProvider();
    //        provider.Visit(predicate);

    //        //var dic = (IDictionary<string, object>)_dynamicModel;

    //        var model = predicate.Compile().Invoke();
    //        var types = model.GetType();


    //        provider.SelectFields.ForEach(t =>
    //        {
    //            var values = types.GetProperty(t.Parameter)?.GetValue(model);

    //            _sqlPamater[t.Parameter] = values;
    //        });
    //    }

    //    public StringBuilder Build(bool ignorePrefix = true)
    //    {
    //        var sql = new StringBuilder();
    //        sql.Append(" where ");

    //        sql.Append(_whereCause);

    //        sql.Append(_whereProvider.whereModel.Sql);
    //        sql.Remove(sql.Length - 3, 3);

    //        foreach (var keyValuePair in _whereProvider.whereModel.Parameter)
    //        {
    //            _sqlPamater[keyValuePair.Key] = keyValuePair.Value;
    //        }

    //        return ignorePrefix ? sql.Replace($"{prefix}.", "") : sql;
    //    }
    //}

    //internal class WhereCommand<T1, T2> : WhereCommand<T1>, IWhereCommand<T1, T2>
    //{
    //    public WhereCommand(Dictionary<string, object> sqlPamater) : base(sqlPamater)
    //    {
    //    }

    //    public void Where(Expression<Func<T1, T2, bool>> predicate)
    //    {
    //        _whereProvider.Visit(predicate);

    //        _whereProvider.whereModel.Sql.Append(" and");

    //        prefix = _whereProvider.whereModel.Prefix;

    //    }
    //}

    //internal class WhereCommand<T1, T2, T3> : WhereCommand<T1, T2>, IWhereCommand<T1, T2, T3>
    //{
    //    public WhereCommand(Dictionary<string, object> sqlPamater) : base(sqlPamater)
    //    {
    //    }

    //    public void Where(Expression<Func<T1, T2, T3, bool>> predicate)
    //    {
    //        _whereProvider.Visit(predicate);

    //        _whereProvider.whereModel.Sql.Append(" and");

    //        prefix = _whereProvider.whereModel.Prefix;
    //    }
    //}
}
