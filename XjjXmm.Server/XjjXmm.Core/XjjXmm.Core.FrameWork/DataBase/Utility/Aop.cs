﻿using System;

namespace DoCare.Extension.DataBase.Utility
{
    public class Aop
    {
        public Action<string, object> OnExecuting { get; set; }

        public Action<string, object> OnExecuted { get; set; }

        public Action<string, object> OnError { get; set; }
    }
}
