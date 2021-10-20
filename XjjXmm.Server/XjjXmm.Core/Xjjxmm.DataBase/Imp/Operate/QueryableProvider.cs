using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ubiety.Dns.Core.Records.NotUsed;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate
{
    internal abstract class QueryableProvider : BaseOperate, IQueryableProvider
    {
        protected readonly string _alias;

        private readonly WhereCommand _whereCommand;
        private readonly IOrderByCommand _orderByCommand;

        private readonly StringBuilder _selectField = new StringBuilder();
        private StringBuilder _joinSql = new StringBuilder();

        private readonly StringBuilder _selectField2 = new StringBuilder();
        private readonly List<string> _splitList = new List<string>();
        private readonly List<Type>  _selectType = new List<Type>();

        public QueryableProvider(DbInfo dbInfo, string alias) : base(dbInfo)
        {

            _alias = alias;

            _whereCommand = new WhereCommand(_providerModel, CreateWhereProvider());

            _orderByCommand = new OrderByCommand();

        }

        protected void VisitSplitOnPredicate<T>(string alias, Expression<Func<T, string>> splitOnPredicate)
        {
            if (splitOnPredicate == null)
            {
                return;
            }

            var provider = new SplitOnProvider();
            provider.Visit(splitOnPredicate);
            _splitList.AddRange(provider.SelectFields.Select(t => t.ColumnName));

            
            var type = typeof(T);
            var (_, properties) = ProviderHelper.GetMetas(type);

            foreach (var property in properties)
            {
                _selectField2.Append($"{alias}.{property.ColumnName} as {property.Parameter},");
            }

            _selectType.Add(typeof(T));
        }

        public void Join<T1, T2>(string alias, Expression<Func<T1, T2, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.Join(predicate);

            _joinSql.Append(joinCommand.Build<T2>());

            //VisitSplitOnPredicate(alias, splitOnPredicate);
        }

        public void Join<T1, T2, T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.Join(predicate);

            _joinSql.Append(joinCommand.Build<T3>());
        }

        public void Join<T1, T2, T3, T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.Join(predicate);

            _joinSql.Append(joinCommand.Build<T4>());
        }


        public void Join<T1, T2, T3, T4, T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.Join(predicate);

            _joinSql.Append(joinCommand.Build<T5>());
        }

        public void Join<T1, T2, T3, T4, T5, T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.Join(predicate);

            _joinSql.Append(joinCommand.Build<T6>());
        }

        public void Join<T1, T2, T3, T4, T5, T6, T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.Join(predicate);

            _joinSql.Append(joinCommand.Build<T7>());
        }

        public void LeftJoin<T1, T2>(string alias, Expression<Func<T1, T2, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.LeftJoin(predicate);

            _joinSql.Append(joinCommand.Build<T2>());

            //VisitSplitOnPredicate(alias, splitOnPredicate);
        }

        public void LeftJoin<T1, T2, T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.LeftJoin(predicate);

            _joinSql.Append(joinCommand.Build<T3>());
        }

        public void LeftJoin<T1, T2, T3, T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.LeftJoin(predicate);

            _joinSql.Append(joinCommand.Build<T4>());
        }


        public void LeftJoin<T1, T2, T3, T4, T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.LeftJoin(predicate);

            _joinSql.Append(joinCommand.Build<T5>());
        }

        public void LeftJoin<T1, T2, T3, T4, T5, T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.LeftJoin(predicate);

            _joinSql.Append(joinCommand.Build<T6>());
        }

        public void LeftJoin<T1, T2, T3, T4, T5, T6, T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
        {
            var joinCommand = CreateJoinCommand(alias, _providerModel); // new JoinCommand(alias, _providerModel);
            joinCommand.LeftJoin(predicate);

            _joinSql.Append(joinCommand.Build<T7>());
        }

        public void Where<T>(Expression<Func<T, bool>> predicate)
        {
            _whereCommand.Where(predicate);
        }

        public void Where<T1, T2>(Expression<Func<T1, T2, bool>> predicate)
        {
            _whereCommand.Where(predicate);
        }

        public void Where<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate)
        {
            _whereCommand.Where(predicate);
        }

        public void Where<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {
            _whereCommand.Where(predicate);
        }

        public void Where<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> predicate)
        {
            _whereCommand.Where(predicate);
        }

        public void Where<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            _whereCommand.Where(predicate);
        }

        public void Where<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
        {
            _whereCommand.Where(predicate);
        }

        public void Where(string whereExpression)
        {
            _whereCommand.Where(whereExpression);
        }

        public void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate)
        {
            _whereCommand.Where(whereExpression, predicate);
        }

        public void OrderBy<T, TResult>(Expression<Func<T, TResult>> predicate)
        {
            _orderByCommand.AscBy(predicate);
        }

        public void OrderBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            _orderByCommand.AscBy(predicate);
        }

        public void OrderBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            _orderByCommand.AscBy(predicate);
        }

        public void OrderBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            _orderByCommand.AscBy(predicate);
        }

        public void OrderBy<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            _orderByCommand.AscBy(predicate);
        }

        public void OrderBy<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            _orderByCommand.AscBy(predicate);
        }

        public void OrderBy<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
        {
            _orderByCommand.AscBy(predicate);
        }

        public void OrderByDesc<T, TResult>(Expression<Func<T, TResult>> predicate)
        {
            _orderByCommand.DescBy(predicate);
        }

        public void OrderByDesc<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            _orderByCommand.DescBy(predicate);
        }

        public void OrderByDesc<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            _orderByCommand.DescBy(predicate);
        }

        public void OrderByDesc<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            _orderByCommand.DescBy(predicate);
        }

        public void OrderByDesc<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            _orderByCommand.DescBy(predicate);
        }

        public void OrderByDesc<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            _orderByCommand.DescBy(predicate);
        }

        public void OrderByDesc<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
        {
            _orderByCommand.DescBy(predicate);
        }


        private IReaderableCommand<TResult> VisitSelect<T, TResult>(Expression predicate)
        {
            var provider = new SelectProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                if (t.Expression != null)
                {
                    var result = ProviderHelper.VisitSqlFuc(t.Expression, CreateSqlFunVisit());
                    _selectField.Append($"{result} as {t.Parameter},");
                }
                else if (string.IsNullOrWhiteSpace(t.Prefix))
                {
                    _selectField.Append($"'{t.ColumnName}' as {t.Parameter},");
                }
                else
                {
                    _selectField.Append($"{t.Prefix}.{t.ColumnName} as {t.Parameter},");
                }



                //prefix = t.Prefix;
            });
            _selectField.Remove(_selectField.Length - 1, 1);

            return CreateReaderableCommand<TResult>();
            //return DatabaseFactory.CreateReaderableCommand<TResult>(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
        }

        public IReaderableCommand<TResult> Select<T, TResult>(Expression<Func<T, TResult>> predicate)
        {
            return VisitSelect<T, TResult>(predicate);
        }


        public IReaderableCommand<TResult> Select<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            return VisitSelect<T1, TResult>(predicate);
        }

        public IReaderableCommand<TResult> Select<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            return VisitSelect<T1, TResult>(predicate);
        }

        public IReaderableCommand<TResult> Select<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            return VisitSelect<T1, TResult>(predicate);
        }


        public IReaderableCommand<TResult> Select<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            return VisitSelect<T1, TResult>(predicate);
        }

        public IReaderableCommand<TResult> Select<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            return VisitSelect<T1, TResult>(predicate);
        }

        public IReaderableCommand<TResult> Select<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
        {
            return VisitSelect<T1, TResult>(predicate);
        }


        private StringBuilder Build<T>()
        {
            //prefix = whereCommand.prefix;

            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, properties) = ProviderHelper.GetMetas(type);

            var selectSql = new StringBuilder();
            if (_selectField.Length > 0)
            {
                selectSql.Append(_selectField);
            }
            else if (_splitList.Count > 0)
            {
                //selectSql.Append("*");
                foreach (var property in properties)
                {
                    selectSql.Append($"{_alias}.{property.ColumnName} as {property.Parameter},");
                }

                selectSql.Append(_selectField2);

                selectSql.Remove(selectSql.Length - 1, 1);

            }
            else
            {
                foreach (var property in properties)
                {
                    selectSql.Append($"{_alias}.{property.ColumnName} as {property.Parameter},");
                }

                selectSql.Remove(selectSql.Length - 1, 1);
            }

            if (tableName.Length > 10 && tableName.Substring(0, 10).ToLower().StartsWith("select"))
            {
                tableName = $"({tableName})";
            }

            sql.Append($"select {selectSql} from {tableName} {_alias} {_joinSql}");


            sql.Append(_whereCommand.Build(false));

            sql.Append(_orderByCommand.Build(false));

            return sql;
        }

        //public async Task<IEnumerable<T>> ExecuteQuery<T>()
        //{
        //    //var command =  _readerableCommandBuilder.Build(Build<T>(), _providerModel.Parameter);
        //    //return await command.ExecuteQuery<T>();

        //    //var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);

        //    var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
        //    return await command.ExecuteQuery<T>();
        //}



        /*   public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2>(Func<T1, T2, T1> func, params string[] splitOn)
           {
               var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
               return await command.ExecuteQuery(func, splitOn);
           }

           public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3>(Func<T1, T2, T3, T1> func, params string[] splitOn)
           {
               var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
               return await command.ExecuteQuery(func, splitOn);
           }

           public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, params string[] splitOn)
           {
               var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
               return await command.ExecuteQuery(func, splitOn);
           }



           public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, params string[] splitOn)
           {
               var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
               return await command.ExecuteQuery(func, splitOn);
           }

           public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, params string[] splitOn)
           {
               var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
               return await command.ExecuteQuery(func, splitOn);
           }

           public async Task<IEnumerable<T1>> ExecuteQuery<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func,
               params string[] splitOn)
           {
               var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
               return await command.ExecuteQuery(func, splitOn);
           }*/

        /*
        public async Task<T> ExecuteFirst<T>()
        {
            //var command = _readerableCommandBuilder.Build(Build<T>(), _providerModel.Parameter);
            //return await command.ExecuteFirst<T>();

            // var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            return await command.ExecuteFirst<T>();
        }

        public async Task<T1> ExecuteFirst<T1, T2>(Func<T1, T2, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirst(func,splitOn);
        }

        public async Task<T1> ExecuteFirst<T1, T2, T3>(Func<T1, T2, T3, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirst(func, splitOn);
        }

        public async Task<T1> ExecuteFirst<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirst(func, splitOn);
        }

        public async Task<T1> ExecuteFirst<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirst(func, splitOn);
        }

        public async Task<T1> ExecuteFirst<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirst(func, splitOn);
        }

        public async Task<T1> ExecuteFirst<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirst(func, splitOn);
        }

        public async Task<T> ExecuteFirstOrDefault<T>()
        {
            //var command = _readerableCommandBuilder.Build(Build<T>(), _providerModel.Parameter);
            //return await command.ExecuteFirstOrDefault<T>();

            // var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            return await command.ExecuteFirstOrDefault<T>();
        }

        public async Task<T1> ExecuteFirstOrDefault<T1, T2>(Func<T1, T2, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirstOrDefault(func, splitOn);
        }

        public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3>(Func<T1, T2, T3, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirstOrDefault(func, splitOn);
        }

        public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirstOrDefault(func, splitOn);
        }

        public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirstOrDefault(func, splitOn);
        }

        public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirstOrDefault(func, splitOn);
        }

        public async Task<T1> ExecuteFirstOrDefault<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T1> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(true), _providerModel.Parameter);
            return await command.ExecuteFirstOrDefault(func, splitOn);
        }

        public async Task<T> ExecuteSingle<T>()
        {
            //var command = _readerableCommandBuilder.Build(Build<T>(), _providerModel.Parameter);
            //return await command.ExecuteSingle<T>();

            // var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            return await command.ExecuteSingle<T>();
        }

        public async Task<T> ExecuteSingleOrDefault<T>()
        {
            //var command = _readerableCommandBuilder.Build(Build<T>(), _providerModel.Parameter);

            //return await command.ExecuteSingleOrDefault<T>();

            //var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            return await command.ExecuteSingleOrDefault<T>();
        }

        public async Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new Exception("pageIndex 不能小于1页");
            }

            if (pageSize < 1)
            {
                throw new Exception("pageSize 不能小于1条");
            }

            //var command = _readerableCommandBuilder.Build(Build<T>(), _providerModel.Parameter);

            //return await command.ToPageList<T>(pageIndex, pageSize);

            //var command = DatabaseFactory.CreateReaderableCommand<T>(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            return await command.ToPageList<T>(pageIndex, pageSize);

        }

        public async Task<DataTable> ExecuteDataTable<T>()
        {
            var command = CreateReaderableCommand(_providerModel.DbInfo, Build<T>(), _providerModel.Parameter);
            return await command.ExecuteDataTable<T>();
        }
        */

        protected abstract IReaderableCommand CreateReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter);

        public IReaderableCommand<TResult> CreateReaderableCommand<TResult>()
        {
            return new ReaderableCommand<TResult>(CreateReaderableCommand(_providerModel.DbInfo, Build<TResult>(), _providerModel.Parameter));
        }

        public IReaderableCommand<T1, T2> CreateReaderableCommand<T1, T2>()
        {
            return new ReaderableCommand<T1, T2>(CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(), _providerModel.Parameter));
        }

        public IReaderableCommand<T1, T2, T3> CreateReaderableCommand<T1, T2, T3>()
        {
            return new ReaderableCommand<T1, T2, T3>(CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(), _providerModel.Parameter));
        }

        public IReaderableCommand<T1, T2, T3, T4> CreateReaderableCommand<T1, T2, T3, T4>()
        {
            return new ReaderableCommand<T1, T2, T3, T4>(CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(), _providerModel.Parameter));
        }

        public IReaderableCommand<T1, T2, T3, T4, T5> CreateReaderableCommand<T1, T2, T3, T4, T5>()
        {
            return new ReaderableCommand<T1, T2, T3, T4, T5>(CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(), _providerModel.Parameter));
        }

        public IReaderableCommand<T1, T2, T3, T4, T5, T6> CreateReaderableCommand<T1, T2, T3, T4, T5, T6>()
        {
            return new ReaderableCommand<T1, T2, T3, T4, T5, T6>(CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(), _providerModel.Parameter));
        }

        public IReaderableCommand<T1, T2, T3, T4, T5, T6, T7> CreateReaderableCommand<T1, T2, T3, T4, T5, T6, T7>()
        {
            return new ReaderableCommand<T1, T2, T3, T4, T5, T6, T7>(CreateReaderableCommand(_providerModel.DbInfo, Build<T1>(), _providerModel.Parameter));
        }

        protected abstract ISqlFuncVisit CreateSqlFunVisit();


        protected abstract WhereProvider CreateWhereProvider();

        protected abstract JoinCommand CreateJoinCommand(string alias, ProviderModel providerModel);


    }

}
