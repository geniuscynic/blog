﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Extension.Dao.Common
{

    public enum DatabaseProvider
    {
        Oracle,
        MsSql,
        MySql
    }

    
    


    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            this.Name = name;

        }
        public string Name  { get; set; }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public bool IsIdentity { get; set; } = false;

        public bool IsPrimaryKey { get; set; } = false;

        public string ColumnName { get; set; }

        public bool Ignore { get; set; }

    }

    public class Member
    {
        public bool IsIdentity { get; set; } = false;
        public bool IsPrimaryKey { get; set; } = false;

        public string ColumnName { get; set; }

        public string Parameter { get; set; }

    }

    public class Field
    {
        public string ColumnName { get; set; }

        public string Parameter { get; set; }

        public string Prefix { get; set; }

        public string Express => !string.IsNullOrEmpty(Prefix) ? $"{Prefix}.{ColumnName}" : ColumnName;
    }

    public class WhereModel
    {
        public StringBuilder Sql { get; set; }    = new StringBuilder();

        public Dictionary<string, object> Parameter { get; set; }     = new Dictionary<string, object>();

        public string Prefix { get; set; } = "";

    }
}
