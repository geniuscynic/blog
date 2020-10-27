using Blog.Common.Extensions.ServiceExtensions;
using Blog.Core.Models;
using Blog.Repository;
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
        public static void UseSeedDataMildd(this IApplicationBuilder app, Dbcontext dbcontext)
        {

            if (AppSettingHelper.App("SeedDBEnabled").ObjToBool())
            {
                dbcontext.Db.DbMaintenance.CreateDatabase();
                dbcontext.Db.CodeFirst.InitTables(typeof(BlogArticle));
                dbcontext.Db.CodeFirst.InitTables(typeof(BlogTag));
                dbcontext.Db.CodeFirst.InitTables(typeof(Category));
                dbcontext.Db.CodeFirst.InitTables(typeof(Tag));


                dbcontext.GetSimpleClient<Category>().Insert(new Category
                {
                    Name = "笑话",
                    SeqNum = 1,

                    Floor = 1,
                    ParentId = 0
                });
                //db.GetSimpleClient<Category>().
            }

        }
    }
}
