﻿using System.Collections.Generic;

namespace Admin.Service.Api
{
    /// <summary>
    /// 接口同步
    /// </summary>
    public class ApiSyncInput
    {
        public List<ApiSyncDto> Apis { get; set; }
    }
}