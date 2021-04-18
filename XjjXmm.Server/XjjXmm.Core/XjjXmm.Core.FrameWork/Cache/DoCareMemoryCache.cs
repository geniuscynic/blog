using System;

namespace DoCare.Extension.Cache
{
    public class DoCareMemoryCache : ICache
    {

        public void Set<T>(string key, T model)
        {
           // CacheKit.Set(key, model);
        }

        public void Set<T>(string key, T model, TimeSpan expried)
        {
            //CacheKit.Set(key, model, expried);
        }

        public T Get<T>(string key)
        {
           //return CacheKit.Get<T>(key);
           throw new Exception();
        }

        public void Remove(string key)
        {
           // CacheKit.Remove(key);
        }
    }
}
