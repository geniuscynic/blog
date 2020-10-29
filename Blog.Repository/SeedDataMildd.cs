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
                dbcontext.Db.CodeFirst.InitTables(typeof(User));
                dbcontext.Db.CodeFirst.InitTables(typeof(Role));
                dbcontext.Db.CodeFirst.InitTables(typeof(UserRole));


                dbcontext.GetSimpleClient<Category>().Insert(new Category
                {
                    Name = "笑话",
                    SeqNum = 1,

                    Floor = 1,
                    ParentId = 0
                });

                dbcontext.GetSimpleClient<Role>().Insert(new Role
                {
                     Code = "superAdmin",
                      Name = "超级管理员"
                });

                dbcontext.GetSimpleClient<Role>().Insert(new Role
                {
                    Code = "admin",
                    Name = "管理员"
                });

                dbcontext.GetSimpleClient<Role>().Insert(new Role
                {
                    Code = "nomal",
                    Name = "普通用户"
                });

                dbcontext.GetSimpleClient<User>().Insert(new User
                {
                     Account = "su",
                     Password = "su",
                      LoginTime = DateTime.Now,
                       NickName = "su11"
                });

                dbcontext.GetSimpleClient<User>().Insert(new User
                {
                    Account = "ad",
                    Password = "ad",
                    LoginTime = DateTime.Now,
                    NickName = "ad11"
                });

                dbcontext.GetSimpleClient<UserRole>().Insert(new UserRole
                {
                     RoleId = 1,
                     UserId = 1
                });

                dbcontext.GetSimpleClient<UserRole>().Insert(new UserRole
                {
                    RoleId = 2,
                    UserId = 2
                });
                //db.GetSimpleClient<Category>().
            }

        }
    }
}
