using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork.DataValidation;

namespace XjjXmm.Authorize.Service.Model
{
    public class AuthUserDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Code { get; set; }

        public string UUID { get; set; }
    }
}
