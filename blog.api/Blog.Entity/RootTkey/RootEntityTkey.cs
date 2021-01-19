﻿using System;
using SqlSugar;

namespace Blog.Entity.RootTkey
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