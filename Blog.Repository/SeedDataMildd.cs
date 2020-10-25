using Blog.Common.Extensions.ServiceExtensions;
using Blog.Core.Models;
using Microsoft.AspNetCore.Builder;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Common.Extensions.Middlewares
{
    public static class SeedDataMildd
    {
        public static void UseSeedDataMildd(this IApplicationBuilder app, ISqlSugarClient db)
        {

            if (AppSettingHelper.App("SeedDBEnabled").ObjToBool())
            {
                db.DbMaintenance.CreateDatabase();
                db.CodeFirst.InitTables(typeof(BlogArticle));
                db.CodeFirst.InitTables(typeof(BlogTag));
                db.CodeFirst.InitTables(typeof(Category));
                db.CodeFirst.InitTables(typeof(Tag));



                //db.GetSimpleClient<Category>().
            }

        }
    }
}
