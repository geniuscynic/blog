using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate
{
    internal class ComplexQueryable<T> : IComplexQueryable<T>
    {
        private readonly IQueryableProvider _provider;


        //private readonly  StringBuilder _sortSql = new StringBuilder();

        public ComplexQueryable(IQueryableProvider provider)
        {
            _provider = provider;
        }


        public IComplexQueryable<T, T2> Join<T2>(string alias, Expression<Func<T, T2, bool>> predicate)
        {
            _provider.Join(alias, predicate);

            return new ComplexQueryable<T, T2>(_provider);
        }

        public IComplexQueryable<T, T2> LeftJoin<T2>(string alias, Expression<Func<T, T2, bool>> predicate)
        {
            _provider.LeftJoin(alias, predicate);

            return new ComplexQueryable<T, T2>(_provider);
        }

        public IComplexQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _provider.Where(predicate);

            return this;

        }

        public IComplexQueryable<T> Where(string whereExpression)
        {
            _provider.Where(whereExpression);

            return this;
        }

        public IComplexQueryable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            _provider.Where(whereExpression, predicate);

            return this;

        }

        public IComplexQueryable<T> OrderBy<TResult>(Expression<Func<T, TResult>> predicate)
        {
            _provider.OrderBy(predicate);


            return this;
        }

        public IComplexQueryable<T> OrderByDesc<TResult>(Expression<Func<T, TResult>> predicate)
        {
            _provider.OrderByDesc(predicate);

            return this;
        }

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate)
        {
            return _provider.Select(predicate);
        }

        private string[] Visit<TSplit>(Expression<Func<TSplit>> splitOnPredicate)
        {
            return new string[] { };
        }


        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            return await _provider.CreateReaderableCommand<T>(false).ExecuteQuery();
        }

        public async Task<IEnumerable<T>> ExecuteQuery< T2, TSelect>(Func<T, T2, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            //var setProvider = new SplitOnProvider();
            //setProvider.Visit(splitOnPredicate);

            ////var dic = (IDictionary<string, object>)_dynamicModel;

            ////var model = predicate.Compile().Invoke();
            ////var types = model.GetType();

            //var splitOn = new List<string>();

            //setProvider.UpdatedFields.ForEach(t =>
            //{
            //    splitOn.Add(t.ColumnName);
            //    //var values = types.GetProperty(t.Parameter)?.GetValue(model);

            //    //setSql.Append($" {t.ColumnName} = {_providerModel.DbInfo.StatementPrefix}{t.Parameter},");

            //    //_providerModel.Parameter[t.Parameter] = values;
            //});

            return await _provider.CreateReaderableCommand<T>(true).ExecuteQuery(func, splitOnPredicate);
        }

      

        public async Task<IEnumerable<T>> ExecuteQuery< T2, T3, TSelect>(Func<T, T2, T3, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteQuery(func, splitOnPredicate);
        }

        public async Task<IEnumerable<T>> ExecuteQuery< T2, T3, T4, TSelect>(Func<T, T2, T3, T4, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteQuery(func, splitOnPredicate);
        }

        public async Task<IEnumerable<T>> ExecuteQuery< T2, T3, T4, T5, TSelect>(Func<T, T2, T3, T4, T5, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteQuery(func, splitOnPredicate);
        }

        public async Task<IEnumerable<T>> ExecuteQuery< T2, T3, T4, T5, T6, TSelect>(Func<T, T2, T3, T4, T5, T6, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteQuery(func, splitOnPredicate);
        }

        public async Task<IEnumerable<T>> ExecuteQuery< T2, T3, T4, T5, T6, T7, TSelect>(Func<T, T2, T3, T4, T5, T6, T7, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteQuery(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirst()
        {
            return await _provider.CreateReaderableCommand<T>(false).ExecuteFirst();
        }

        public async Task<T> ExecuteFirst<T2, TSelect>(Func<T, T2, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirst(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirst<T2, T3, TSelect>(Func<T, T2, T3, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirst(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirst<T2, T3, T4, TSelect>(Func<T, T2, T3, T4, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirst(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirst<T2, T3, T4, T5, TSelect>(Func<T, T2, T3, T4, T5, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirst(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirst<T2, T3, T4, T5, T6, TSelect>(Func<T, T2, T3, T4, T5, T6, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirst(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirst<T2, T3, T4, T5, T6, T7, TSelect>(Func<T, T2, T3, T4, T5, T6, T7, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirst(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            return await _provider.CreateReaderableCommand<T>(false).ExecuteFirstOrDefault();
        }

        public async Task<T> ExecuteFirstOrDefault<T2, TSelect>(Func<T, T2, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirstOrDefault(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirstOrDefault<T2, T3, TSelect>(Func<T, T2, T3, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirstOrDefault(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirstOrDefault<T2, T3, T4, TSelect>(Func<T, T2, T3, T4, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirstOrDefault(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirstOrDefault<T2, T3, T4, T5, TSelect>(Func<T, T2, T3, T4, T5, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirstOrDefault(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirstOrDefault<T2, T3, T4, T5, T6, TSelect>(Func<T, T2, T3, T4, T5, T6, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirstOrDefault(func, splitOnPredicate);
        }

        public async Task<T> ExecuteFirstOrDefault<T2, T3, T4, T5, T6, T7, TSelect>(Func<T, T2, T3, T4, T5, T6, T7, T> func, Expression<Func<TSelect>> splitOnPredicate)
        {
            return await _provider.CreateReaderableCommand<T>(true).ExecuteFirstOrDefault(func, splitOnPredicate);
        }

        public async Task<T> ExecuteSingle()
        {
            return await _provider.CreateReaderableCommand<T>(false).ExecuteSingle();
          
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            return await _provider.CreateReaderableCommand<T>(false).ExecuteSingleOrDefault();

        }

        public async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            return await _provider.CreateReaderableCommand<T>(false).ToPageList(pageIndex, pageSize);
        }

        public async Task<DataTable> ExecuteDataTable()
        {
            return await _provider.CreateReaderableCommand<T>(false).ExecuteDataTable();
        }
    }

    internal class ComplexQueryable<T1, T2> : ComplexQueryable<T1>, IComplexQueryable<T1, T2>
    {
        private readonly IQueryableProvider _provider;

        public ComplexQueryable(IQueryableProvider provider) : base(provider)
        {
            _provider = provider;
        }



        public IComplexQueryable<T1, T2, T3> Join<T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate)
        {

            _provider.Join(alias, predicate);


            return new ComplexQueryable<T1, T2, T3>(_provider);
        }

        public IComplexQueryable<T1, T2, T3> LeftJoin<T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate)
        {

            _provider.LeftJoin(alias, predicate);


            return new ComplexQueryable<T1, T2, T3>(_provider);
        }

        public new IComplexQueryable<T1, T2> Where(string whereExpression)
        {
            _provider.Where(whereExpression);

            return this;
        }


        public IComplexQueryable<T1, T2> Where(Expression<Func<T1, T2, bool>> predicate)
        {
            _provider.Where(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2> OrderBy<TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            _provider.OrderBy(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2> OrderByDesc<TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            _provider.OrderByDesc(predicate);

            return this;
        }

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            return _provider.Select(predicate);
        }
    }


    internal class ComplexQueryable<T1, T2, T3> : ComplexQueryable<T1, T2>, IComplexQueryable<T1, T2, T3>
    {
        //private readonly StringBuilder _join;

        private readonly IQueryableProvider _provider;

        public ComplexQueryable(IQueryableProvider provider) : base(provider)
        {
            _provider = provider;
        }


        public IComplexQueryable<T1, T2, T3, T4> Join<T4>(string alias,
            Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {

            _provider.Join(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4>(_provider);
        }

        public IComplexQueryable<T1, T2, T3, T4> LeftJoin<T4>(string alias,
            Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {

            _provider.LeftJoin(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4>(_provider);
        }

        public new IComplexQueryable<T1, T2, T3> Where(string whereExpression)
        {
            _provider.Where(whereExpression);

            return this;
        }

        public IComplexQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> predicate)
        {
            _provider.Where(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3> OrderBy<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            _provider.OrderBy(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            _provider.OrderByDesc(predicate);

            return this;
        }

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            return _provider.Select(predicate);
        }
    }

    internal class ComplexQueryable<T1, T2, T3, T4> : ComplexQueryable<T1, T2, T3>, IComplexQueryable<T1, T2, T3, T4>
    {
        //private readonly StringBuilder _join;

        private readonly IQueryableProvider _provider;

        public ComplexQueryable(IQueryableProvider provider) : base(provider)
        {
            _provider = provider;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5> Join<T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate)
        {
            _provider.Join(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4, T5>(_provider);
        }

        public IComplexQueryable<T1, T2, T3, T4, T5> LeftJoin<T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate)
        {
            _provider.LeftJoin(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4, T5>(_provider);
        }

        public new IComplexQueryable<T1, T2,T3,T4> Where(string whereExpression)
        {
            _provider.Where(whereExpression);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> predicate)
        {
            _provider.Where(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            _provider.OrderBy(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            _provider.OrderByDesc(predicate);

            return this;
        }

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            return _provider.Select(predicate);
        }
    }

    internal class ComplexQueryable<T1, T2, T3, T4,T5> : ComplexQueryable<T1, T2, T3,T4>, IComplexQueryable<T1, T2, T3, T4, T5>
    {
        //private readonly StringBuilder _join;

        private readonly IQueryableProvider _provider;

        public ComplexQueryable(IQueryableProvider provider) : base(provider)
        {
            _provider = provider;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6> Join<T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            _provider.Join(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4, T5, T6>(_provider);
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6> LeftJoin<T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            _provider.LeftJoin(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4, T5, T6>(_provider);
        }

        public new IComplexQueryable<T1, T2, T3, T4, T5> Where(string whereExpression)
        {
            _provider.Where(whereExpression);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> predicate)
        {
            _provider.Where(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            _provider.OrderBy(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            _provider.OrderByDesc(predicate);

            return this;
        }

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            return _provider.Select(predicate);
        }
    }


    internal class ComplexQueryable<T1, T2, T3, T4, T5, T6> : ComplexQueryable<T1, T2, T3, T4, T5>, IComplexQueryable<T1, T2, T3, T4, T5, T6>
    {
        //private readonly StringBuilder _join;

        private readonly IQueryableProvider _provider;

        public ComplexQueryable(IQueryableProvider provider) : base(provider)
        {
            _provider = provider;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> Join<T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
        {
            _provider.Join(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4, T5, T6, T7>(_provider);
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> LeftJoin<T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
        {
            _provider.LeftJoin(alias, predicate);


            return new ComplexQueryable<T1, T2, T3, T4, T5, T6, T7>(_provider);
        }


        public new IComplexQueryable<T1, T2, T3, T4, T5, T6> Where(string whereExpression)
        {
            _provider.Where(whereExpression);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            _provider.Where(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            _provider.OrderBy(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            _provider.OrderByDesc(predicate);

            return this;
        }

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            return _provider.Select(predicate);
        }
    }


    internal class ComplexQueryable<T1, T2, T3, T4, T5, T6, T7> : ComplexQueryable<T1, T2, T3, T4, T5, T6>, IComplexQueryable<T1, T2, T3, T4, T5, T6, T7>
    {
        //private readonly StringBuilder _join;

        private readonly IQueryableProvider _provider;

        public ComplexQueryable(IQueryableProvider provider) : base(provider)
        {
            _provider = provider;
        }

        public new IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> Where(string whereExpression)
        {
            _provider.Where(whereExpression);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6,  T7, bool>> predicate)
        {
            _provider.Where(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
        {
            _provider.OrderBy(predicate);

            return this;
        }

        public IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
        {
            _provider.OrderByDesc(predicate);

            return this;
        }

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
        {
            return _provider.Select(predicate);
        }
    }
}
