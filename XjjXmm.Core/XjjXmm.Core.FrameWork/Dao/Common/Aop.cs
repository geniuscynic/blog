using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Extension.Dao.Common
{
    public class Aop
    {
        public Action<string, object> OnExecuting { get; set; }

        public Action<string, object> OnExecuted { get; set; }

        public Action<string, object> OnError { get; set; }
    }
}
