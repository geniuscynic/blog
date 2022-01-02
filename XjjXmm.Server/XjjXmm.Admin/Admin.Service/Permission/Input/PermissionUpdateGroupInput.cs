﻿namespace Admin.Service.Permission.Input
{
    public class PermissionUpdateGroupInput : PermissionAddGroupInput
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public long Version { get; set; }
    }
}