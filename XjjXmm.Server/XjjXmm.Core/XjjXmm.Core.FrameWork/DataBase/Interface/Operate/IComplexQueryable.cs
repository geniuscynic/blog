using System;
using System.Linq.Expressions;
using DoCare.Extension.DataBase.Interface.Command;

namespace DoCare.Extension.DataBase.Interface.Operate
{
    public interface IQueryableProvider : IReaderableCommand
    {
        void Join<T1, T2>(string alias, Expression<Func<T1, T2, bool>> predicate);

        void Join<T1, T2, T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);

        void Join<T1, T2, T3, T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void LeftJoin<T1, T2>(string alias, Expression<Func<T1, T2, bool>> predicate);
        void LeftJoin<T1, T2, T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);
        void LeftJoin<T1, T2, T3, T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void Where<T>(Expression<Func<T, bool>> predicate);

        void Where<T1,  T2>(Expression<Func<T1, T2, bool>> predicate);

        void Where<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate);

        void Where<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate);

        void Where(string whereExpression);

        void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        
        void OrderBy<T, TResult>(Expression<Func<T, TResult>> predicate);

        void OrderBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void OrderBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void OrderBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        void OrderByDesc<T, TResult>(Expression<Func<T, TResult>> predicate);

        void OrderByDesc<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        void OrderByDesc<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        IReaderableCommand<TResult> Select<T, TResult>(Expression<Func<T, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2,TResult>(Expression<Func<T1,T2, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        IReaderableCommand<TResult> Select<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);
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

    public interface IComplexQueryable<T1, T2>  : IComplexQueryable<T1>
    {
        IComplexQueryable<T1, T2, T3> Join<T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);

        IComplexQueryable<T1, T2, T3> LeftJoin<T3>(string alias, Expression<Func<T1, T2, T3, bool>> predicate);

        IComplexQueryable<T1, T2> Where(Expression<Func<T1, T2, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2> OrderBy<TResult>(Expression<Func<T1, T2, TResult>> predicate);

        IComplexQueryable<T1, T2> OrderByDesc<TResult>(Expression<Func<T1, T2, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> predicate);

    }

    public interface IComplexQueryable<T1, T2, T3> : IComplexQueryable<T1, T2>
    {

        IComplexQueryable<T1, T2, T3, T4> Join<T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        IComplexQueryable<T1, T2, T3, T4> LeftJoin<T4>(string alias, Expression<Func<T1, T2, T3, T4, bool>> predicate);

        IComplexQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2, T3> OrderBy<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        IComplexQueryable<T1, T2, T3> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> predicate);

    }

    public interface IComplexQueryable<T1, T2, T3, T4> : IComplexQueryable<T1, T2, T3>
    {
        IComplexQueryable<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> predicate);

        //IComplexQueryable<T1, T2> Where(string whereExpression);

        //IComplexQueryable<T1, T2> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4> OrderBy<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        IComplexQueryable<T1, T2, T3, T4> OrderByDesc<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

        IReaderableCommand<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate);

    }
}
