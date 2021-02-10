using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IRepository;
using SqlSugar;

namespace Blog.Repository
{
    public class ButtonRepository: Repository<Button> , IButtonRepository
    {
        public ButtonRepository(Dbcontext dbcontext) : base(dbcontext)
        {
        }

        public async Task<List<Button>> GetButtons(string token)
        {
            return await Db.Queryable<Button, ButtonPermission, Role>((t, mp, r) => new JoinQueryInfos(
                    JoinType.Inner, t.Id == mp.ButtonId,
                    JoinType.Inner, mp.RoleId == r.Id  //SqlFunc.ContainsArray(jwt.Role, r.Code)
                ))
                .ToListAsync();
        }

        public async Task<List<Button>> GetButtonsByRole(List<string> roles)
        {
            return await Db.Queryable<Button, ButtonPermission, Role>((t, mp, r) => new JoinQueryInfos(
                    JoinType.Inner, t.Id == mp.ButtonId,
                    JoinType.Inner, mp.RoleId == r.Id  //SqlFunc.ContainsArray(jwt.Role, r.Code)
                ))
                .ToListAsync();
        }
    }
}
