using Admin.Repository.Permission;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace Admin.Repository
{
    public interface IRepositoryQuery<T>  where T : class, new()
    {
        IRepositoryQuery<T> Where(Expression<Func<T, bool>> expression);

        IRepositoryQuery<T> WhereIf(bool isWhere, Expression<Func<T, bool>> expression);

        IRepositoryQuery<T> OrderBytAsc(Expression<Func<T, object>> expression);

        IRepositoryQuery<T> OrderBytAsc(bool isOrderBy, Expression<Func<T, object>> expression);

        IRepositoryQuery<T> OrderBytDesc(Expression<Func<T, object>> expression);

        IRepositoryQuery<T> OrderBytDesc(bool isOrderBy, Expression<Func<T, object>> expression);




        T First();

        List<T> ToList();

        PageOutput<T> ToPage(int currentPage, int pageSize);
    }
}
