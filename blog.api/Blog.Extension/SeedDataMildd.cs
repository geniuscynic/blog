using Blog.Common.Extensions.ServiceExtensions;
using Blog.Repository;
using Microsoft.AspNetCore.Builder;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.Extension.Extensions.ServiceExtensions;

namespace Blog.Common.Extensions.Middlewares
{
    public static class SeedDataMildd
    {
        public static void UseSeedDataMildd(this IApplicationBuilder app, Dbcontext dbcontext)
        {

            if (AppSettingHelper.App("SeedDBEnabled:table").ObjToBool())
            {
                dbcontext.Db.DbMaintenance.CreateDatabase();
                dbcontext.Db.CodeFirst.InitTables(typeof(BlogArticle));
                dbcontext.Db.CodeFirst.InitTables(typeof(BlogTag));
                dbcontext.Db.CodeFirst.InitTables(typeof(Category));
                dbcontext.Db.CodeFirst.InitTables(typeof(Tag));
                dbcontext.Db.CodeFirst.InitTables(typeof(User));
                dbcontext.Db.CodeFirst.InitTables(typeof(Role));
                dbcontext.Db.CodeFirst.InitTables(typeof(UserRole));
                dbcontext.Db.CodeFirst.InitTables(typeof(Menu));
                dbcontext.Db.CodeFirst.InitTables(typeof(MenuPermission));
                dbcontext.Db.CodeFirst.InitTables(typeof(Button));
                dbcontext.Db.CodeFirst.InitTables(typeof(ButtonPermission));
                dbcontext.Db.CodeFirst.InitTables(typeof(ApiMethod));
                dbcontext.Db.CodeFirst.InitTables(typeof(ApiMethodPermission));
            }

            if (AppSettingHelper.App("SeedDBEnabled:data").ObjToBool())
            {

                if (!dbcontext.GetSimpleClient<Category>().GetList().Any())
                {
                    dbcontext.GetSimpleClient<Category>().Insert(new Category
                    {
                        Name = "笑话",
                        SeqNum = 1,

                        Floor = 1,
                        ParentId = 0
                    });
                }

                if (!dbcontext.GetSimpleClient<Role>().GetList().Any())
                {
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
                }

                if (!dbcontext.GetSimpleClient<Menu>().GetList().Any())
                {
                    dbcontext.GetSimpleClient<Menu>().InsertRange(new Menu[] {
                        new Menu
                        {
                            Id=1,
                            Code="home",
                            Name="XJJXMM欢迎页",
                            Icon="icon-home",
                            ParentId=0,
                            Route = "",
                            SeqNum = 1
                        },
                        new Menu
                        {
                            Id=2,
                            Code="us",
                            Name="用户管理",
                            Icon="icon-home",
                            ParentId=0,
                            Route = "",
                            SeqNum = 1
                        },
                        new Menu
                        {
                            Id=3,
                            Code="user",
                            Name="用户管理",
                            Icon="icon-home",
                            ParentId=2,
                            Route = "/user",
                            SeqNum = 1
                        },
                        new Menu
                        {
                            Id=4,
                            Code="role",
                            Name="角色管理",
                            Icon="icon-home",
                            ParentId=2,
                            Route = "/role",
                            SeqNum = 1
                        },
                        new Menu
                        {
                            Id=5,
                            Code="mu",
                            Name="菜单管理",
                            Icon="icon-home",
                            ParentId=0,
                            Route = "",
                            SeqNum = 1
                        },
                        new Menu
                        {
                            Id=6,
                            Code="menu",
                            Name="菜单管理",
                            Icon="icon-home",
                            ParentId=5,
                            Route = "/menu",
                            SeqNum = 1
                        },
                        new Menu
                        {
                            Id=7,
                            Code="perm",
                            Name="权限管理",
                            Icon="icon-home",
                            ParentId=5,
                            Route = "/perm",
                            SeqNum = 1
                        }
                    });
                }

                if(!dbcontext.GetSimpleClient<MenuPermission>().GetList().Any())
                {
                    dbcontext.GetSimpleClient<MenuPermission>().InsertRange(new MenuPermission[] {
                        new MenuPermission
                        {
                            Id=1,
                            MenuId = 1,
                            RoleId = 1
                        },
                        new MenuPermission
                        {
                            Id=2,
                            MenuId = 2,
                            RoleId = 1
                        },
                        new MenuPermission
                        {
                            Id=3,
                            MenuId = 3,
                            RoleId = 1
                        },


                    });
                }
                //db.GetSimpleClient<Category>().
            }

        }
    }
}
