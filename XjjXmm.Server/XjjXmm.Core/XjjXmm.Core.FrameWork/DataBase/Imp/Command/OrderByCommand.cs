using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DoCare.Extension.DataBase.Interface.Command;
using DoCare.Extension.DataBase.SqlProvider;


namespace DoCare.Extension.DataBase.Imp.Command
{
    internal class OrderByCommand : IOrderByCommand
    {
        private readonly StringBuilder _sortSql = new StringBuilder();
        private string prefix = "";

        private void VisitOrderBy(Expression predicate)
        {
            var provider = new SelectProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _sortSql.Append($"{t.Prefix}.{t.ColumnName},");

                prefix = t.Prefix;

            });
        }
       

        public void VisitOrderByDesc(Expression predicate)
        {
            var provider = new SelectProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _sortSql.Append($"{t.Prefix}.{t.ColumnName} desc,");

                prefix = t.Prefix;

            });
        }

        public void OrderBy<T, TResult>(Expression<Func<T, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void OrderBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void OrderBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }


        public void OrderBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void OrderByDesc<T, TResult>(Expression<Func<T, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void OrderByDesc<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void OrderByDesc<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void OrderByDesc<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public StringBuilder Build(bool ignorePrefix = true)
        {
            if (_sortSql.Length <= 0) return _sortSql;


            var sql = new StringBuilder();
            sql.Append(" order by ");
            sql.Append(_sortSql);
            sql.Remove(sql.Length - 1, 1);

            if (ignorePrefix)
            {
                sql = sql.Replace($"{prefix}.", "");
            }

            return sql;
        }

    }


    internal class OrderByCommand<T> : IOrderByCommand<T>
    {
        private readonly StringBuilder _sortSql = new StringBuilder();
        private string prefix = "";
        public void OrderBy<TResult>(Expression<Func<T, TResult>> predicate)
        {
            var provider = new SelectProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _sortSql.Append($"{t.Prefix}.{t.ColumnName},");

                prefix = t.Prefix;

            });
        }

        public void OrderByDesc<TResult>(Expression<Func<T, TResult>> predicate)
        {
            var provider = new SelectProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _sortSql.Append($"{t.Prefix}.{t.ColumnName} desc,");

                prefix = t.Prefix;

            });
        }

        public StringBuilder Build(bool ignorePrefix = true)
        {
            if (_sortSql.Length <= 0) return _sortSql;


            var sql = new StringBuilder();
            sql.Append(" order by ");
            sql.Append(_sortSql);
            sql.Remove(sql.Length - 1, 1);

            if (ignorePrefix)
            {
                sql = sql.Replace($"{prefix}.", "");
            }

            return sql;

        }
    }
}
