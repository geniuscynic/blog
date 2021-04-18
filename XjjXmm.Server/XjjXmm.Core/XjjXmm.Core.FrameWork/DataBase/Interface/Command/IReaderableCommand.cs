﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Extension.DataBase.Interface.Command
{
    public interface IReaderableCommand
    {
        Task<IEnumerable<T>> ExecuteQuery<T>();

        Task<T> ExecuteFirst<T>();

        Task<T> ExecuteFirstOrDefault<T>();

        Task<T> ExecuteSingle<T>();

        Task<T> ExecuteSingleOrDefault<T>();

        Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize);
    }

    public interface IReaderableCommand<T>
    {
        Task<IEnumerable<T>> ExecuteQuery();

        Task<T> ExecuteFirst();

        Task<T> ExecuteFirstOrDefault();

        Task<T> ExecuteSingle();

        Task<T> ExecuteSingleOrDefault();

        Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize);
    }
}
