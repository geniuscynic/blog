using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Interface.Operate
{
    internal interface IQueryableProvider
    {
        void Join<T1, T2>(string alias, Expression<Func<T1, T2, bool>> predicate);

        void Join<T1, T2, T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);

        void Join<T1, T2, T3, T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void Join<T1, T2, T3, T4, T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate);

        void Join<T1, T2, T3, T4, T5, T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate);

        void Join<T1, T2, T3, T4, T5, T6, T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate);



        void LeftJoin<T1, T2>(string alias, Expression<Func<T1, T2, bool>> predicate);
        void LeftJoin<T1, T2, T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);
        void LeftJoin<T1, T2, T3, T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void LeftJoin<T1, T2, T3, T4, T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate);

        void LeftJoin<T1, T2, T3, T4, T5, T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate);

        void LeftJoin<T1, T2, T3, T4, T5, T6, T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate);


        void Where<T>(Expression<Func<T, bool>> predicate);

        void Where<T1,  T2>(Expression<Func<T1, T2, bool>> predicate);

        void Where<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate);

        void Where<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void Where<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> predicate);

        void Where<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate);

        void Where<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate);

        void Where(string whereExpression);

        void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        
        void OrderBy<T, TResult>(Expression<Func<T, TResult>> predicate);

        void OrderBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void OrderBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void OrderBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        void OrderBy<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate);

        void OrderBy<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate);

        void OrderBy<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate);

        void OrderByDesc<T, TResult>(Expression<Func<T, TResult>> predicate);


        void OrderByDesc<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate);

        IReaderableCommand<TResult> Select<T, TResult>(Expression<Func<T, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2,TResult>(Expression<Func<T1,T2, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);


        IReaderableCommand<TResult> Select<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate);

        IReaderableCommand<T> CreateReaderableCommand<T>();

        IReaderableCommand<T1, T2> CreateReaderableCommand<T1, T2>();

        IReaderableCommand<T1, T2, T3> CreateReaderableCommand<T1, T2, T3>();

        IReaderableCommand<T1, T2, T3, T4> CreateReaderableCommand<T1, T2, T3, T4>();

        IReaderableCommand<T1, T2, T3, T4, T5> CreateReaderableCommand<T1, T2, T3, T4, T5>();

        IReaderableCommand<T1, T2, T3, T4, T5, T6> CreateReaderableCommand<T1, T2, T3, T4, T5, T6>();

        IReaderableCommand<T1, T2, T3, T4, T5, T6, T7> CreateReaderableCommand<T1, T2, T3, T4, T5, T6, T7>();
    }

    public interface IComplexQueryable<T> : IReaderableCommand<T>
    {
        IComplexQueryable<T, T2> Join<T2>(string alias, Expression<Func<T, T2, bool>> predicate);

        IComplexQueryable<T, T2> LeftJoin<T2>(string alias, Expression<Func<T, T2, bool>> predicate);

        IComplexQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IComplexQueryable<T> Where(string whereExpression);

        IComplexQueryable<T> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T> OrderBy<TResult>(Expression<Func<T, TResult>> predicate);

        IComplexQueryable<T> OrderByDesc<TResult>(Expression<Func<T, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate);


    }

    public interface IComplexQueryable<T1, T2>  : IComplexQueryable<T1>//, IReaderableCommand<T1,T2>
    {
        IComplexQueryable<T1, T2, T3> Join<T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);

        IComplexQueryable<T1, T2, T3> LeftJoin<T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);

        new IComplexQueryable<T1, T2> Where(string whereExpression);

        IComplexQueryable<T1, T2> Where(Expression<Func<T1, T2, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2> OrderBy<TResult>(Expression<Func<T1, T2, TResult>> predicate);

        IComplexQueryable<T1, T2> OrderByDesc<TResult>(Expression<Func<T1, T2, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> predicate);


        //Task<IEnumerable<T1>> ExecuteQuery<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirst<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate);

        //Task<T1> ExecuteFirstOrDefault<TSecond, TSplit>(Expression<Func<T1, TSecond, TSplit>> splitOnPredicate);

    }

    public interface IComplexQueryable<T1, T2, T3> : IComplexQueryable<T1, T2>//, IReaderableCommand<T1, T2,T3>
    {

        IComplexQueryable<T1, T2, T3, T4> Join<T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        IComplexQueryable<T1, T2, T3, T4> LeftJoin<T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        new IComplexQueryable<T1, T2, T3> Where(string whereExpression);

        IComplexQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2, T3> OrderBy<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        IComplexQueryable<T1, T2, T3> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

    }

    public interface IComplexQueryable<T1, T2, T3, T4> : IComplexQueryable<T1, T2, T3>//, IReaderableCommand<T1, T2, T3,T4>
    {
        IComplexQueryable<T1, T2, T3, T4, T5> Join<T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5> LeftJoin<T5>(string alias, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate);

        new IComplexQueryable<T1, T2, T3, T4> Where(string whereExpression);

        IComplexQueryable<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

    }

    public interface IComplexQueryable<T1, T2, T3, T4, T5> : IComplexQueryable<T1, T2, T3, T4>//, IReaderableCommand<T1, T2, T3, T4,T5>
    {

        IComplexQueryable<T1, T2, T3, T4, T5, T6> Join<T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5, T6> LeftJoin<T6>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate);


        new IComplexQueryable<T1, T2, T3, T4, T5> Where(string whereExpression);

        IComplexQueryable<T1, T2, T3, T4, T5> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate);

    }


    public interface IComplexQueryable<T1, T2, T3, T4, T5, T6> : IComplexQueryable<T1, T2, T3, T4, T5>//, IReaderableCommand<T1, T2, T3, T4, T5, T6>
    {
        IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> Join<T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> LeftJoin<T7>(string alias, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate);


        new IComplexQueryable<T1, T2, T3, T4, T5, T6> Where(string whereExpression);

        IComplexQueryable<T1, T2, T3, T4, T5, T6> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5, T6> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5, T6> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate);

    }

    public interface IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> : IComplexQueryable<T1, T2, T3, T4, T5, T6> //, IReaderableCommand<T1, T2, T3, T4, T5, T6, T7>
    {
        new IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> Where(string whereExpression);

        IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4, T5, T6, T7> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate);

    }
}
