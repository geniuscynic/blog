using System;

namespace XjjXmm.FrameWork.DependencyInjection
{
    /// <summary>
    /// 设置依赖注入方式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectionAttribute : Attribute
    {
        public InjectionType Type { get; set; } = InjectionType.Scoped;
    }

    public enum InjectionType
    {
        Scoped,
        Singleton,
        Transient

    }
}