using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons =  new List<Person>
                {
                    new Person { ID = 1, Name="Mehfuz Hossain", Age=27},
                    new Person { ID = 2, Name="Json Born", Age=30},
                    new Person { ID = 3, Name="John Doe", Age=52}
                };

            Expression<Func<Person, bool>> expression = t => t.Age < 40 && t.Age > 35;

            //var age = 10;
            //              var query = new Queryable<Person>();
            //              var sql = query.Where(t => t.Age < 10 && t.ID > 5)
            //                  //.Where(t=>t.ID == 22)
            //                  .Select(t => new
            //                  {
            //                      a = t.Name,
            //                      b = t.Age
            //                  })
            //                  .Select(t=>t)
            //                  .Build();

            var connectionString =
                "Server=localhost;Database=blog;Trusted_Connection=True;MultipleActiveResultSets=true";
            var dbContext = new Dbcontext(connectionString);
            var result = dbContext.Queryable<BlogArticle>()
                .Where(t => t.Id > 2)
                //.Where(t=>t.ID == 22)
                //.Select(t => new
                //{
                //    a = t.Title,
                //    b = t.Id
                //})
                .Select(t => t)
                .ToList()
                .FirstOrDefault();

           // dbContext.Insertable(result).Execute();
           dbContext.Updateable(result).UpdateColumns(t => t.Title);

            var a = "";
            //Console.WriteLine(sql);

            //var mycontext = new Mycontext<Person>();
            //var query = mycontext//.Where(t => t.Age < 40 && t.Age > 35)
            //    .Where(t => t.Name == "nnn")
            //    .Where(t => t.Name == "bbb")
            //    //        .Select(t => new {
            //    //            aa = t.Name,
            //    //            bb = t.Age,
            //    //            cc = new {
            //    //            dd = t.ID
            //    //        }
            //    //})
            //    .ToList();


            //var query = context.Where(p => p.Age < 40);

            //var query = from p in new PersonContext()
            //    where p.Age < 40
            //    select p;


            Console.ReadLine();

           
        }
    }
}
