using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.SqlProvider;


namespace XjjXmm.DataBase.Imp.Command
{
    internal class OrderByCommand : IOrderByCommand
    {
        private readonly StringBuilder _sortSql = new StringBuilder();
        private string prefix = "";

        private void VisitOrderBy(Expression predicate)
        {
            var provider = new OrderByProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _sortSql.Append($"{t.Prefix}.{t.ColumnName},");

                prefix = t.Prefix;

            });
        }
       

        public void VisitOrderByDesc(Expression predicate)
        {
            var provider = new OrderByProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _sortSql.Append($"{t.Prefix}.{t.ColumnName} desc,");

                prefix = t.Prefix;

            });
        }

        public void AscBy<T, TResult>(Expression<Func<T, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void AscBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void AscBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }


        public void AscBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }


        public void AscBy<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void AscBy<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void AscBy<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
        {
            VisitOrderBy(predicate);
        }

        public void DescBy<T, TResult>(Expression<Func<T, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void DescBy<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void DescBy<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void DescBy<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }


        public void DescBy<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void DescBy<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> predicate)
        {
            VisitOrderByDesc(predicate);
        }

        public void DescBy<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> predicate)
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
        public void AscBy<TResult>(Expression<Func<T, TResult>> predicate)
        {
            var provider = new OrderByProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _sortSql.Append($"{t.Prefix}.{t.ColumnName},");

                prefix = t.Prefix;

            });
        }

        public void DescBy<TResult>(Expression<Func<T, TResult>> predicate)
        {
            var provider = new OrderByProvider();
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
