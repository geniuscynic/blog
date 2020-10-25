﻿using SqlSugar;
using System;

namespace Blog.Core.Models
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    public class RootEntityTkey<Tkey> where Tkey : IEquatable<Tkey>
    {
        /// <summary>
        /// ID
        /// 泛型主键Tkey
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public Tkey Id { get; set; }


    }
}