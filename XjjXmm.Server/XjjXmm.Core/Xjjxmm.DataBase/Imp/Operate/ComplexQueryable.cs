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
using Xjjxmm.DataBase.Utility;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate
{
    internal class ComplexQueryable<T> : IComplexQueryable<T>
    {
        private readonly IQueryableProvider _provider;

        private MappingHelper<T> _mapHelper;
        //private readonly  StringBuilder _sortSql = new StringBuilder();

        public ComplexQueryable(IQueryableProvider provider)
        {
            _provider = provider;
            _mapHelper = new MappingHelper<T>(_provider);
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

        //public IReaderableCommand<T> Include<T2>(Expression<Func<T, int>> predicate1, Expression<Func<T2, int>> predicate2, Expression<Func<T, T2>> mappingFunc)
        //{
        //    throw new NotImplementedException();
        //}

        //public IReaderableCommand<T> Include<T2>(Expression<Func<T, long>> predicate1, Expression<Func<T2, long>> predicate2, Expression<Func<T, T2>> mappingFunc)
        //{
        //    throw new NotImplementedException();
        //}

        
        public IComplexQueryable<T> Include<T2>(MappingEntity<T, T2> mapping1)
        {

            _mapHelper
                .AddMapping(mapping1);
            // .BuildKey()
            // .BuilderProvider(mapping1.SubClassKey);

            return this;
        }


        public async Task<IEnumerable<T>> ExecuteMultiQuery()
        {
            //var resulsts = await _provider.CreateReaderableCommand<T>().ExecuteQuery();

            //_mapHelper.Build(resulsts);

            return await _mapHelper.Exec();

        }

     /*   public IEnumerable<T> ExecuteMultiQuery<T2>(MappingEntity<T, T2> mapping1)
        {
            var mapHelper = new MappingHelper<T>(_provider);
            mapHelper
                .AddProperty(mapping1.MainClassKey)
                .BuildKey()
                .BuilderProvider(mapping1.SubClassKey);

            var resT2 = mapHelper.cloneProviders[0].CreateReaderableCommand<T2>().ExecuteQuery().Result.ToList();

            foreach (var result in mapHelper.resultsList)
            {
                var tmp1 = mapping1.MapExpression(result, resT2);


                yield return tmp1;
            }

            //var provider = new SplitOnProvider();
            //provider.Visit(predicate1);
            //var id1 = provider.SelectFields.Select(t => t.Parameter).First();

            //provider = new SplitOnProvider();
            //provider.Visit(predicate2);
            //var id2 = provider.SelectFields.Select(t => t.ColumnName).First();

            //var includeProvider = (IQueryableProvider)_provider.Clone();

            //var includeCOmplexQUertyable = new ComplexQueryable<T2>(includeProvider);

            //var results = _provider.CreateReaderableCommand<T>().ExecuteQuery().Result.ToList();
            //// var results = resultsENumerable.ToList();


            //var property = typeof(T).GetProperty(mapping.MainClassKey);
            //var ids = new StringBuilder();
            //foreach (var res in results)
            //{
            //    ids.Append($"'{property.GetValue(res)}',");
            //}

            //ids.Remove(ids.Length - 1, 1);

            //var includeProvider = (IQueryableProvider)_provider.Clone();
            //includeProvider.Where($"{mapping.SubClassKey} in ({ids})");
            //var resT2 = includeProvider.CreateReaderableCommand<T2>().ExecuteQuery().Result;

            //foreach (var result in results)
            //{
            //    yield return (T)mapping.MapExpression(result, resT2);
            //}

            //return results;

        }
*/
       /* public IEnumerable<T> ExecuteMultiQuery<T2, T3>(MappingEntity<T, T2> mapping1, MappingEntity<T, T3> mapping2)
        {

            var mapHelper = new MappingHelper<T>(_provider);
            mapHelper.AddProperty(mapping1.MainClassKey)
            .AddProperty(mapping2.MainClassKey)
            .BuildKey()
            .BuilderProvider(mapping1.SubClassKey, mapping2.SubClassKey);

            var resT2 = mapHelper.cloneProviders[0].CreateReaderableCommand<T2>().ExecuteQuery().Result.ToList();
            var resT3 = mapHelper.cloneProviders[1].CreateReaderableCommand<T3>().ExecuteQuery().Result.ToList();

            foreach (var result in mapHelper.resultsList)
            {
                var tmp1 = mapping1.MapExpression(result, resT2);
                var tmp2 = mapping2.MapExpression(tmp1, resT3);

                yield return tmp2;
            }

            // var mappintList = new[] {mapping1, mapping2};

            //var results = _provider.CreateReaderableCommand<T>().ExecuteQuery().Result.ToList();

            //var property1 = typeof(T).GetProperty(mapping1.MainClassKey);
            //var property2 = typeof(T).GetProperty(mapping2.MainClassKey);

            //var ids1 = new StringBuilder();
            //var ids2 = new StringBuilder();
            //foreach (var res in results)
            //{
            //    ids1.Append($"'{property1.GetValue(res)}',");
            //    ids2.Append($"'{property2.GetValue(res)}',");
            //}

            //ids1.Remove(ids1.Length - 1, 1);
            //ids2.Remove(ids2.Length - 1, 1);

            //var includeProvider1 = (IQueryableProvider)_provider.Clone();
            //includeProvider1.Where($"{mapping1.SubClassKey} in ({ids1})");
            //var resT2 = includeProvider1.CreateReaderableCommand<T2>().ExecuteQuery().Result;

            //var includeProvider2 = (IQueryableProvider)_provider.Clone();
            //includeProvider2.Where($"{mapping2.SubClassKey} in ({ids2})");
            //var resT3 = includeProvider2.CreateReaderableCommand<T3>().ExecuteQuery().Result;

            //foreach (var result in results)
            //{
            //    var tmp1 = mapping1.MapExpression(result, resT2);
            //    var tmp2 = mapping2.MapExpression(tmp1, resT3);

            //    yield return (T)tmp2;
            //}

            //return results;

        }

        public IEnumerable<T> ExecuteMultiQuery<T2, T3, T4>(MappingEntity<T, T2> mapping1, MappingEntity<T, T3> mapping2, MappingEntity<T, T4> mapping3)
        {

            var mapHelper = new MappingHelper<T>(_provider);
            mapHelper
                .AddProperty(mapping1.MainClassKey)
            .AddProperty(mapping2.MainClassKey)
                .AddProperty(mapping3.MainClassKey)
            .BuildKey()
            .BuilderProvider(mapping1.SubClassKey, mapping2.SubClassKey, mapping3.SubClassKey);

            var resT2 = mapHelper.cloneProviders[0].CreateReaderableCommand<T2>().ExecuteQuery().Result;
            var resT3 = mapHelper.cloneProviders[1].CreateReaderableCommand<T3>().ExecuteQuery().Result;
            var resT4 = mapHelper.cloneProviders[2].CreateReaderableCommand<T3>().ExecuteQuery().Result;

            foreach (var result in mapHelper.resultsList)
            {
                var tmp1 = mapping1.MapExpression(result, resT2);
                var tmp2 = mapping2.MapExpression(tmp1, resT3);
                var tmp3 = mapping2.MapExpression(tmp2, resT4);

                yield return tmp3;
            }


        }
*/

        public async Task<IEnumerable<T>> ExecuteQuery()
        {


            return await _provider.CreateReaderableCommand<T>().ExecuteQuery();
        }

        public async Task<T> ExecuteFirst()
        {
            return await _provider.CreateReaderableCommand<T>().ExecuteFirst();
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            return await _provider.CreateReaderableCommand<T>().ExecuteFirstOrDefault();
        }

        public async Task<T> ExecuteSingle()
        {
            return await _provider.CreateReaderableCommand<T>().ExecuteSingle();

        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            return await _provider.CreateReaderableCommand<T>().ExecuteSingleOrDefault();

        }

        public async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            return await _provider.CreateReaderableCommand<T>().ToPageList(pageIndex, pageSize);
        }

        public async Task<DataTable> ExecuteDataTable()
        {
            return await _provider.CreateReaderableCommand<T>().ExecuteDataTable();
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

        //public async Task<IEnumerable<T1>> ExecuteQuery<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate)
        //{
        //    return await _provider.CreateReaderableCommand<T1, TSecond>().ExecuteQuery();
        //}

        //public async Task<T1> ExecuteFirst<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate)
        //{
        //    return await _provider.CreateReaderableCommand<T1, TSecond>().ExecuteFirst();
        //}

        //public async Task<T1> ExecuteFirstOrDefault<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate)
        //{
        //    return await _provider.CreateReaderableCommand<T1, TSecond>().ExecuteFirstOrDefault();
        //}

        /*        public async Task<IEnumerable<T1>> ExecuteQuery<TResult>()
                {
                    return await _provider.CreateReaderableCommand<T1,T2>(true).ExecuteQuery();
                }

                public async Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, TResult>> splitOnPredicate)
                {
                    return await _provider.CreateReaderableCommand<T1, T2>(true).ExecuteFirst( splitOnPredicate);
                }

                public async Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, TResult>> splitOnPredicate)
                {
                    return await _provider.CreateReaderableCommand<T1, T2>(true).ExecuteFirstOrDefault( splitOnPredicate);
                }*/

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



        /*     public async Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, TResult>> splitOnPredicate)
             {
                 return await _provider.CreateReaderableCommand<T1, T2, T3>(true).ExecuteQuery( splitOnPredicate);
             }

             public async Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, TResult>> splitOnPredicate)
             {
                 return await _provider.CreateReaderableCommand<T1, T2, T3>(true).ExecuteFirst( splitOnPredicate);
             }

             public async Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, TResult>> splitOnPredicate)
             {
                 return await _provider.CreateReaderableCommand<T1, T2, T3>(true).ExecuteFirstOrDefault( splitOnPredicate);
             }*/
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

        public new IComplexQueryable<T1, T2, T3, T4> Where(string whereExpression)
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

        /*  public async Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4>(true).ExecuteQuery( splitOnPredicate);
          }

          public async Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4>(true).ExecuteFirst( splitOnPredicate);
          }

          public async Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4>(true).ExecuteFirstOrDefault( splitOnPredicate);
          }*/

    }

    internal class ComplexQueryable<T1, T2, T3, T4, T5> : ComplexQueryable<T1, T2, T3, T4>, IComplexQueryable<T1, T2, T3, T4, T5>
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

        /*  public async Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5>(true).ExecuteQuery( splitOnPredicate);
          }

          public async Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5>(true).ExecuteFirst( splitOnPredicate);
          }

          public async Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5>(true).ExecuteFirstOrDefault( splitOnPredicate);
          }*/
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

        /*  public async Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5,T6>(true).ExecuteQuery( splitOnPredicate);
          }

          public async Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5, T6>(true).ExecuteFirst( splitOnPredicate);
          }

          public async Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate)
          {
              return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5, T6>(true).ExecuteFirstOrDefault( splitOnPredicate);
          }*/
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

        public IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
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

        /* public async Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate)
         {
             return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5, T6, T7>(true).ExecuteQuery( splitOnPredicate);
         }

         public async Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate)
         {
             return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5, T6, T7>(true).ExecuteFirst( splitOnPredicate);
         }

         public async Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate)
         {
             return await _provider.CreateReaderableCommand<T1, T2, T3, T4, T5, T6, T7>(true).ExecuteFirstOrDefault( splitOnPredicate);
         }*/
    }
}
