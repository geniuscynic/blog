﻿using System.Data;

namespace DoCare.Extension.Dao.Imp.Operate.MySqlOperate
{
    public class MySqlDeleteable<T> : Deleteable<T>
    {
        public MySqlDeleteable(IDbConnection connection) : base(connection)
        {


        }
    }
}
