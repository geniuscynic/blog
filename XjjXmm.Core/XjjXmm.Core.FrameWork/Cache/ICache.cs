using System;

namespace DoCare.Extension.Cache
{
    public interface ICache
    {
        void Set<T>(string key, T model);

        void Set<T>(string key, T model, TimeSpan exprie);

        T Get<T>(string key);

        void Remove(string key);
    }
}
