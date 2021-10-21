using System;
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

            var res = dbClient.ComplexQueryable<FormEntity>("p")
                .Join<FormDefaultEntity>("e", (p, e) => p.Id == e.FormId)
                .ExecuteQuery<FormDefaultEntity>((p, e) => new
                {
                    id = e.Id
                })

                //.ExecuteQuery( (p, e) => e.Id)
                .Result;

            Console.WriteLine("Hello World!");
        }
    }
}
