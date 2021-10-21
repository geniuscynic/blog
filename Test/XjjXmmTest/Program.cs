using System;
using System.Dynamic;
using System.Linq.Expressions;
using Dapper;
using XjjXmm.DataBase;
using XjjXmmTest.entity;

namespace XjjXmmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectString = "Data Source=ZKZX;Persist Security Info=True;User ID=medcomm;Password=medcomm";
            var provider = "Oracle";
            var dbClient = new DbClient(connectString, provider);


            //var res = dbClient.ComplexQueryable<FormEntity>("p")
            //    .Join<FormDefaultEntity>("e", (p, e) => p.Id == e.FormId)
            //    .ExecuteQuery<FormDefaultEntity>((p, e) => new
            //    {
            //        id = e.Id
            //    })

            var sql = "select * from med_fum_form m join MED_FUM_FORM_DEFAULT t on m.id = t.form_id";

            var types = new Type[] { typeof(FormEntity), typeof(FormDefaultEntity) };

            var first = Expression.Parameter(types[0], "first1");
            var second = Expression.Parameter(types[1], "second1");

            var p1 = Expression.Parameter(typeof(object[]));
            var secondSetExpression = MappingCache.GetSetExpression(second, first);

            // var blockExpression = Expression.Block(first, second, secondSetExpression, first);
            var blockExpression = Expression.Block(p1, secondSetExpression, first);
            var map = Expression.Lambda<Func<object[], FormEntity>>(blockExpression,  p1).Compile();

            Expression.Lambda<Func<object[], FormEntity>>(secondSetExpression, first, second).Compile();
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
            //    .Join<FormDefaultEntity>("e", (p, e) => p.Id == e.FormId)
            //    .ExecuteQuery<FormDefaultEntity,object>((p, e) => new
            //    {
            //        id = e.Id
            //    })

            //    //.ExecuteQuery( (p, e) => e.Id)
            //    .Result;

            Console.WriteLine("Hello World!");
        }
    }
}
