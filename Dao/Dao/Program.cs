using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.Dao;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString =
                "Server=localhost;Database=blog;Trusted_Connection=True;MultipleActiveResultSets=true";
            var dbContext = new Dbclient(connectionString);

            var result = new List<BlogArticle>()
            {
                new BlogArticle()
                {
                    Id=1027,
                    Title = "aa",
                    Author1 = "bb3",
                    CategoryId = 1,
                    Content = "content",
                    PublishDate = DateTime.Now,
                    Quote = "quote",
                    Test = "test"
                },
                new BlogArticle()
                {
                    Id=1026,
                    Title = "aa1",
                    Author1 = "bb4",
                    CategoryId = 2,
                    Content = "content1",
                    PublishDate = DateTime.Now,
                    Quote = "quote1",
                    Test = "test1"
                },

            };


            var c = "tst";
            //await dbContext.Saveable(result).UpdateColumns(t =>
            //    new {
            //        t.Author1,
            //        t.Content

            //    }
            //).Execute();

            await dbContext.Updateable<BlogArticle>().SetColumns(() => new BlogArticle
            {
                Title = "aa",
                Author1 = "bb3",
                Content = c,
                PublishDate = DateTime.Now

            })
                .Where("id > 10 || content = 'c'")
                .Where("id > 10 || content = @content", () => new
                {
                    content = c
                })
                //.Where(t => (t.Id >10 || t.Content == c) && t.PublishDate > DateTime.Now)
                //.Where(t => t.CategoryId == 2)
                .Execute();

            Console.WriteLine("Hello World!");

            Console.Read();
        }
    }
}
