using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Model
{
    public class MenuDto
    {
        public long Id { get; set; }

        public List<MenuDto> Children { get; set; }

        public int Type { get; set; }

        public string Permission { get; set; }

        public string Title { get; set; }

        public int MenuSort { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public long? Pid { get; set; }

        public int SubCount { get; set; }

        public bool IFrame { get; set; }

        public bool Cache { get; set; }

        public bool Hidden { get; set; }

        public string ComponentName { get; set; }

        public string Icon { get; set; }

        public bool HasChildren => SubCount > 0;
        
        public bool Leaf => SubCount <= 0;

        public string Label => Title;
    }
}
