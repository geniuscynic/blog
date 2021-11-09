using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Vo
{
    public class MenuVo
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public bool Hidden { get; set; }

        public string Redirect { get; set; }

        public string Component { get; set; }

        public bool AlwaysShow { get; set; }

        public MenuMetaVo Meta { get; set; }

        public List<MenuVo> Children { get; set; }
    }
}
