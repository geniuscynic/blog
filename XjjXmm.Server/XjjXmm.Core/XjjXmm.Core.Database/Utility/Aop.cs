using System;

namespace DoCare.Zkzx.Core.Database.Utility
{
    public class Aop
    {
        public Action<string, object> OnExecuting { get; set; }

        public Action<string, object> OnExecuted { get; set; }

        public Action<string, object, Exception> OnError { get; set; }
    }
}
