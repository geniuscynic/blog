using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IRepository
{
    public interface IUnitOfWork
    {
        void BeginTran();

        void Commit();

        void Rollback();
    }
}
