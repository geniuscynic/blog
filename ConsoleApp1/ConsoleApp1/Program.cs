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
            


            var context = new PersonContext();

            var mycontext = new Mycontext<Person>();
            var query = mycontext.Where(t => t.Age < 40 && t.Age > 35)
                .Where(t => t.Name == "nnn").First();
             

            //var query = context.Where(p => p.Age < 40);

            //var query = from p in new PersonContext()
            //    where p.Age < 40
            //    select p;

          
            Console.ReadLine();

            Console.WriteLine("Hello World!");
        }
    }
}
