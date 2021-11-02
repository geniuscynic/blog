using System;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Microsoft.VisualBasic;
using XjjXmm.DataBase;
using XjjXmm.DataBase.Imp.Operate;
using Xjjxmm.DataBase.Utility;
using XjjXmmTest.blog;
using XjjXmmTest.entity;

namespace XjjXmmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectString = "Server=localhost;Database=blog;Trusted_Connection=True;MultipleActiveResultSets=true";
            var provider = "MsSql";
            var dbClient = new DbClient(connectString, provider);

           
                var blog = dbClient.ComplexQueryable<BlogUserEntity>("p")
                    .Include<BlogUserRoleEntity, BlogRoleEntity>(p => p.BlogRoles,
                        p => p.Id,
                        p => p.UserId,
                        p => p.RoleId,
                        p => p.Id)
                    .ExecuteQuery().Result;
           

            //var blog = dbClient.ComplexQueryable<BlogUserRoleEntity>("p")
            //    .Include<BlogUserEntity>(p => p.User, p => p.UserId, p => p.Id)
            //    .ExecuteMultiQuery().Result;
            //var blog = dbClient.ComplexQueryable<BlogUserRoleEntity>("p")
            //    .Include<BlogUserEntity>(new MappingOneToManyEntity<BlogUserRoleEntity, BlogUserEntity>(
            //        p => p.UserId,
            //        e => e.Id,
            //        (t, e) =>
            //        {
            //            t.User = e.FirstOrDefault(a=>a.Id == t.UserId);
            //            //var form = e.FirstOrDefault(r => r.FormId == t.Id);

            //            //t.FormDefaultEntity = e;
            //            return t;
            //        }
            //    ))
            //    .Include<BlogRoleEntity>(new MappingOneToManyEntity<BlogUserRoleEntity, BlogRoleEntity>(
            //        p => p.RoleId,
            //        e => e.Id,
            //        (t, e) =>
            //        {
            //            t.Role = e.FirstOrDefault(a => a.Id == t.RoleId);
            //            //var form = e.FirstOrDefault(r => r.FormId == t.Id);

            //            //t.FormDefaultEntity = e;
            //            return t;
            //        }
            //    ))
            //    .ExecuteMultiQuery().Result;

            //var res = blog.ToList();


            //var connectString = "Data Source=ZKZX;Persist Security Info=True;User ID=medcomm;Password=medcomm";
            //var provider = "Oracle";
            //var dbClient = new DbClient(connectString, provider);


            //var res = dbClient.ComplexQueryable<FormEntity>("p")
            //    .Include<FormDefaultEntity>(p=>p.FormDefaultEntity,
            //        p => p.Id,
            //        e => e.FormId
            //    )
            //  .ExecuteMultiQuery().Result;

            //var res = dbClient.ComplexQueryable<UserEntity>("u")
            //    .Include<GroupUserEntity, GroupEntity>(t => t.Groups,
            //        t => t.Id,
            //        t => t.UserId,
            //        (gu, g) => gu.GroupId == g.Id)
            //    .ExecuteMultiQuery();

            //var sql = "select * from med_fum_form m join MED_FUM_FORM_DEFAULT t on m.id = t.form_id";

            //var types = new Type[] { typeof(FormEntity), typeof(FormDefaultEntity) };

            //var first = Expression.Parameter(types[0], "first1");
            //var second = Expression.Parameter(types[1], "second1");

            //var p1 = Expression.Parameter(typeof(object[]));
            //var secondSetExpression = MappingCache.GetSetExpression(second, first);

            //var delegateType = typeof(Func<,,>).MakeGenericType(typeof(FormEntity), typeof(FormDefaultEntity), typeof(FormEntity));
            //var yourExpression = Expression.Lambda(delegateType, secondSetExpression, first, second).Compile();
            ////var blockExpression = Expression.Block(first, second, secondSetExpression, first);
            //var blockExpression = Expression.Block(p1, secondSetExpression, first);

            //dbClient.GetConnection().Query()
            // var map = Expression.Lambda<Func<T1, T2, T3>>(blockExpression,  p1).Compile();
            //var map = Expression.Lambda<Func<object[],>>(blockExpression, p1).Compile();
            //Expression.Lambda<Func<object[], FormEntity>>(secondSetExpression, first, second).Compile();
            //var res = dbClient.GetConnection().Query<FormEntity>(sql,
            //    map);
            //(o) =>
            //{
            //    var fe = (FormEntity)o[0];
            //    var de = (FormDefaultEntity)o[1];
            //   // fe.FormDefaultEntity = de;
            //    return fe;
            //});
            //var res = dbClient.ComplexQueryable<FormEntity>("p")
            //    .ExecuteMultiQuery<FormDefaultEntity>(new Mapping<FormEntity, FormDefaultEntity>(
            //        p => p.Id, e => e.FormId,
            //        (t, e) =>
            //        {
            //            var form = e.FirstOrDefault(r => r.FormId == t.Id);

            //            t.FormDefaultEntity = form;
            //            return t;
            //        }));

            //var res = dbClient.ComplexQueryable<FormEntity>("p")
            //    .Include<FormDefaultEntity>(new MappingOneToOneEntity<FormEntity, FormDefaultEntity>(
            //        p => p.Id,
            //        e => e.FormId,
            //        (t, e) =>
            //        {
            //            //var form = e.FirstOrDefault(r => r.FormId == t.Id);

            //            t.FormDefaultEntity = e;
            //            return t;
            //        }
            //    ))
            //  .ExecuteMultiQuery().Result;


            //var res = dbClient.ComplexQueryable<UserEntity>("u")
            //    .ExecuteMultiQuery(new MappingEntity<UserEntity,T2>())
            //var res = dbClient.ComplexQueryable<FormEntity>("p")
            //    .ExecuteMultiQuery<FormDefaultEntity>(new MappingEntity<FormEntity, FormDefaultEntity>(
            //        p => p.Id,
            //        e => e.FormId ,
            //        (t, e) =>
            //        {
            //            var form = e.FirstOrDefault(r => r.FormId == t.Id);

            //            t.FormDefaultEntity = form;
            //            return t;
            //        }
            //    ));

            //  var tmp = res.ToList();
            //.Join<FormDefaultEntity>("e", (p, e) => p.Id == e.FormId)
            //.Include<FormDefaultEntity>(p => p.Id,
            //     e => e.FormId,
            //     p=>p.FormDefaultEntity )
            //(t, e) =>
            //{
            //    var form = e.FirstOrDefault(r => r.FormId == t.Id);

            //    t.FormDefaultEntity = form;
            //     return t;
            //})
            // .ExecuteQuery()

            //.ExecuteQuery( (p, e) => e.Id)
            // .Result;

            // var formIds = res.Select(t => t.Id);
            // var res2 = dbClient.ComplexQueryable<FormDefaultEntity>("p")
            //     .Where(p => SqlFunc.Contain(formIds, p.FormId))
            //     .ExecuteQuery().Result;

            //var res3 = res.Select(t =>
            // {
            //     var form = res2.FirstOrDefault(r => r.FormId == t.Id);
            //     if (form != null)
            //     {
            //         t.FormDefaultEntity = form;
            //         return t;
            //     }

            //     return null;
            // }).Where(t=>t != null);

            Console.WriteLine("Hello World!");
        }
    }

    class Test1
    {
        
    }
}
