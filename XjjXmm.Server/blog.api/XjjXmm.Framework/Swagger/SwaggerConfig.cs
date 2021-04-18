using System;
using System.Collections.Generic;
using System.Text;

namespace XjjXmm.Framework.Swagger
{
    public class SwaggerConfig
    {
        public string Version { get; set; } = "v1";
        public string Title { get; set; } = "blog 文档";
        public string Description  {get; set;} = "学习专用";

        public string ContactName  {get; set;} = "XJJXMM";
        public string ContactEmail { get; set; } = "Blog.Core@xxx.com";
        public string ContactUrl { get; set; } = "https://www.xjjxmm.com";


        public string LicenseName { get; set; } = "XJJXMM";
       
        public string LicenseUrl { get; set; } = "https://www.xjjxmm.com";

    }
}
