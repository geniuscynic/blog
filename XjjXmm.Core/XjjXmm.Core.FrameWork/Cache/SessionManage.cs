using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DoCare.Extension.Cache
{
    public class SessionManage : ICache
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        private ISession Session => HttpContextAccessor.HttpContext.Session;

        public void Set<T>(string key, T model)
        {
            if (model is string)
            {
                Session.SetString(key, model.ToString());
            }
            else
            {
                Session.SetObjectAsJson(key, model);
            }
            
        }

        public void Set<T>(string key, T model, TimeSpan exprie)
        {
            Set(key, model);
        }

        public T Get<T>(string key)
        {
            return Session.GetObjectFromJson<T>(key);
        }

        public void Remove(string key)
        {
            Session.Remove(key);
        }
    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

