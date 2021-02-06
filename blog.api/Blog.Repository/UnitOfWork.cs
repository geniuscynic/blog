using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.IRepository;

namespace Blog.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dbcontext _dbcontext;

        public UnitOfWork(Dbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void BeginTran()
        {
            _dbcontext.Db.Ado.BeginTran();
        }

        public void Commit()
        {
            _dbcontext.Db.Ado.CommitTran();
        }

        public void Rollback()
        {
            _dbcontext.Db.Ado.RollbackTran();
        }
    }
}
