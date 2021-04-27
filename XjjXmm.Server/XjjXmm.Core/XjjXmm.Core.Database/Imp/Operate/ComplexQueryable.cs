using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Imp.Command;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate
{
    public class ComplexQueryable<T> : IComplexQueryable<T>
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




        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            return await _provider.ExecuteQuery<T>();
        }

        public async Task<T> ExecuteFirst()
        {
            return await _provider.ExecuteFirst<T>();
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            return await _provider.ExecuteFirstOrDefault<T>();
        }

        public async Task<T> ExecuteSingle()
        {
            return await _provider.ExecuteSingle<T>();
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            return await _provider.ExecuteSingleOrDefault<T>();
        }

        public async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {

            return await _provider.ToPageList<T>(pageIndex, pageSize);

        }

    }

    public class ComplexQueryable<T1, T2> : ComplexQueryable<T1>, IComplexQueryable<T1, T2>
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


    public class ComplexQueryable<T1, T2, T3> : ComplexQueryable<T1, T2>, IComplexQueryable<T1, T2, T3>
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

    public class ComplexQueryable<T1, T2, T3, T4> : ComplexQueryable<T1, T2, T3>, IComplexQueryable<T1, T2, T3, T4>
    {
        //private readonly StringBuilder _join;

        private readonly IQueryableProvider _provider;

        public ComplexQueryable(IQueryableProvider provider) : base(provider)
        {
            _provider = provider;
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
}
