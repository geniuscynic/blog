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

            dbClient.ComplexQueryable<PlanEntity>("p")
                .Join<FormDefaultEntity>("f", (p, e) => p.PlanId == e.Id)
                .ExecuteQuery((p, e) => p, (p, e) => e.Id);

            Console.WriteLine("Hello World!");
        }
    }
}
