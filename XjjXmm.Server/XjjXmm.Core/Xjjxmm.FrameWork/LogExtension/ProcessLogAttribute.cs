using System;

namespace XjjXmm.FrameWork.LogExtension
{
    /// <summary>
    /// 这个Attribute就是使用时候的验证，把它添加到要缓存数据的方法中，即可完成缓存的操作。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
    public class ProcessLogAttribute : Attribute
    {
       

    }
}
