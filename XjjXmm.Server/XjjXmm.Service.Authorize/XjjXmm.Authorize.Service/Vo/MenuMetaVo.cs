using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Vo
{
    public class MenuMetaVo 
    {

        public MenuMetaVo(string title, string icon, bool noCache)
        {
            this.Title = title;
            this.Icon = icon;
            this.NoCache = noCache;
        }

        public string Title { get; set; }

        public string Icon { get; set; }

        public bool NoCache { get; set; }
    }
}
