using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmmTest
{
    public class DMC
    {
        private HashSet<IObject> myObject= new HashSet<IObject>();


        public void Add(IObject objects)
        {
            myObject.Add(objects);
        }

        public List<IObject> Get()
        {
            return myObject.ToList();
        }
    }

    public interface IObject
    {
         public string Name { get; }
    }

    public class TV : IObject
    {
        public string Name { get; } = "TV";

        public string ButtonName { get; set; }
    }

    public class Laptop : IObject
    {
        public string Name { get; } = "Laptop";

        public string ButtonName { get; set; }
    }

    public class Persions : IObject
    {
        public string Name { get; } = "Persions";

        public string ButtonName { get; set; }
    }
}
