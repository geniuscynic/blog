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
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;
using Xjjxmm.DataBase.Utility.MappingCache;

namespace XjjXmm.DataBase.Imp.Command
{
    internal class ReaderableCommand<T> : IReaderableCommand<T>
    {
        protected readonly IReaderableCommand _readerableCommand;

        public ReaderableCommand(IReaderableCommand readerableCommand)
        {
            _readerableCommand = readerableCommand;
        }

        protected string Visit(Expression splitOnPredicate)
        {
            var provider = new SplitOnProvider();
            provider.Visit(splitOnPredicate);
            return string.Join(',', provider.SelectFields.Select(t => t.ColumnName));

        }


        public Task<IEnumerable<T>> ExecuteQuery()
        {
            return _readerableCommand.ExecuteQuery<T>();
        }

        public Task<T> ExecuteFirst()
        {

            return _readerableCommand.ExecuteFirst<T>();
        }

        public Task<T> ExecuteFirstOrDefault()
        {
            return _readerableCommand.ExecuteFirstOrDefault<T>();
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

    internal class ReaderableCommand<T1, T2> : ReaderableCommand<T1>, IReaderableCommand<T1, T2>
    {
        public ReaderableCommand(IReaderableCommand readerableCommand) : base(readerableCommand)
        {
        }

        public Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(map, splitOn);
        }

        public Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(map, splitOn);
        }

        public Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirstOrDefault(map, splitOn);
        }
    }

    internal class ReaderableCommand<T1, T2, T3> : ReaderableCommand<T1, T2>, IReaderableCommand<T1, T2, T3>
    {
        public ReaderableCommand(IReaderableCommand readerableCommand) : base(readerableCommand)
        {
        }

        public Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2,T3>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(map, splitOn);
        }

        public Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(map, splitOn);
        }

        public Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirstOrDefault(map, splitOn);
        }
    }

    internal class ReaderableCommand<T1, T2, T3, T4> : ReaderableCommand<T1, T2, T3>, IReaderableCommand<T1, T2, T3, T4>
    {
        public ReaderableCommand(IReaderableCommand readerableCommand) : base(readerableCommand)
        {
        }


        public Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(map, splitOn);
        }

        public Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(map, splitOn);
        }

        public Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirstOrDefault(map, splitOn);
        }
    }

    internal class ReaderableCommand<T1, T2, T3, T4, T5> : ReaderableCommand<T1, T2, T3, T4>, IReaderableCommand<T1, T2, T3, T4, T5>
    {
        public ReaderableCommand(IReaderableCommand readerableCommand) : base(readerableCommand)
        {
        }

        public Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(map, splitOn);
        }

        public Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(map, splitOn);
        }

        public Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, T5, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirstOrDefault(map, splitOn);
        }
    }

    internal class ReaderableCommand<T1, T2, T3, T4, T5, T6> : ReaderableCommand<T1, T2, T3, T4, T5>, IReaderableCommand<T1, T2, T3, T4, T5, T6>
    {
        public ReaderableCommand(IReaderableCommand readerableCommand) : base(readerableCommand)
        {
        }

        public Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5, T6>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(map, splitOn);
        }

        public Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5, T6>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(map, splitOn);
        }

        public Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5, T6>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirstOrDefault(map, splitOn);
        }
    }

    internal class ReaderableCommand<T1, T2, T3, T4, T5, T6, T7> : ReaderableCommand<T1, T2, T3, T4, T5, T6>, IReaderableCommand<T1, T2, T3, T4, T5, T6, T7>
    {
        public ReaderableCommand(IReaderableCommand readerableCommand) : base(readerableCommand)
        {
        }

        public Task<IEnumerable<T1>> ExecuteQuery<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5, T6,T7>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteQuery(map, splitOn);
        }

        public Task<T1> ExecuteFirst<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5, T6, T7>.Map;
            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirst(map, splitOn);
        }

        public Task<T1> ExecuteFirstOrDefault<TResult>( Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> splitOnPredicate)
        {
            var map = MappingCache<T1, T2, T3, T4, T5, T6, T7>.Map;

            var splitOn = Visit(splitOnPredicate);

            return _readerableCommand.ExecuteFirstOrDefault(map, splitOn);
        }
    }
}
