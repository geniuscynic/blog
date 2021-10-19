using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command
{
    internal class ReaderableCommand<T> : IReaderableCommand<T>
    {
        private readonly IReaderableCommand _readerableCommand;
        
        public ReaderableCommand(IReaderableCommand readerableCommand)
        {
            _readerableCommand = readerableCommand;
        }

        public Task<IEnumerable<T>> ExecuteQuery()
        {
            return _readerableCommand.ExecuteQuery<T>();
        }

        private string[] Visit<TSplit>(Expression<Func<TSplit>> splitOnPredicate)
        {
            return new string[] {};
        }

        public Task<IEnumerable<T>> ExecuteQuery<T2, TResult>(Func<T, T2, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(func, splitOn);
        }

        public Task<IEnumerable<T>> ExecuteQuery<T2, T3, TResult>(Func<T, T2, T3, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(func, splitOn);
        }

        public Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, TResult>(Func<T, T2, T3, T4, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(func, splitOn);
        }

        public Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5, TResult>(Func<T, T2, T3, T4, T5, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(func, splitOn);
        }

        public Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5, T6, TResult>(Func<T, T2, T3, T4, T5, T6, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(func, splitOn);
        }

        public Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5, T6, T7, TResult>(Func<T, T2, T3, T4, T5, T6, T7, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(func, splitOn);
        }

        public Task<T> ExecuteFirst()
        {

            return _readerableCommand.ExecuteFirst<T>();
        }

        public Task<T> ExecuteFirst<T2, TResult>(Func<T, T2, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(func, splitOn);
        }

        public Task<T> ExecuteFirst<T2, T3, TResult>(Func<T, T2, T3, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(func, splitOn);
        }

        public Task<T> ExecuteFirst<T2, T3, T4, TResult>(Func<T, T2, T3, T4, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(func, splitOn);
        }

        public Task<T> ExecuteFirst<T2, T3, T4, T5, TResult>(Func<T, T2, T3, T4, T5, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(func, splitOn);
        }

        public Task<T> ExecuteFirst<T2, T3, T4, T5, T6, TResult>(Func<T, T2, T3, T4, T5, T6, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(func, splitOn);
        }

        public Task<T> ExecuteFirst<T2, T3, T4, T5, T6, T7, TResult>(Func<T, T2, T3, T4, T5, T6, T7, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(func, splitOn);
        }

        public Task<T> ExecuteFirstOrDefault()
        {
            return _readerableCommand.ExecuteFirstOrDefault<T>();
        }

        public Task<T> ExecuteFirstOrDefault<T2, TResult>(Func<T, T2, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);
            return _readerableCommand.ExecuteFirstOrDefault(func, splitOn);
        }

        public Task<T> ExecuteFirstOrDefault<T2, T3, TResult>(Func<T, T2, T3, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);
            return _readerableCommand.ExecuteFirstOrDefault(func, splitOn);
        }

        public Task<T> ExecuteFirstOrDefault<T2, T3, T4, TResult>(Func<T, T2, T3, T4, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);
            return _readerableCommand.ExecuteFirstOrDefault(func, splitOn);
        }

        public Task<T> ExecuteFirstOrDefault<T2, T3, T4, T5, TResult>(Func<T, T2, T3, T4, T5, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);
            return _readerableCommand.ExecuteFirstOrDefault(func, splitOn);
        }

        public Task<T> ExecuteFirstOrDefault<T2, T3, T4, T5, T6, TResult>(Func<T, T2, T3, T4, T5, T6, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);
            return _readerableCommand.ExecuteFirstOrDefault(func, splitOn);
        }

        public Task<T> ExecuteFirstOrDefault<T2, T3, T4, T5, T6, T7, TResult>(Func<T, T2, T3, T4, T5, T6, T7, T> func, Expression<Func<TResult>> splitOnPredicate)
        {
            var splitOn = Visit(splitOnPredicate);
            return _readerableCommand.ExecuteFirstOrDefault(func, splitOn);
        }

        public Task<T> ExecuteSingle()
        {
            return _readerableCommand.ExecuteSingle<T>();
        }

        public Task<T> ExecuteSingleOrDefault()
        {
            return _readerableCommand.ExecuteSingleOrDefault<T>();
        }

        public Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            return _readerableCommand.ToPageList<T>(pageIndex, pageSize);
        }

        public Task<DataTable> ExecuteDataTable()
        {
            return _readerableCommand.ExecuteDataTable<T>();
        }
    }
}
