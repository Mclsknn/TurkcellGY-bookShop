﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace bookShop.Web.Extensions
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            string result = session.GetString(key);
            return result == null ? default(T) : JsonConvert.DeserializeObject<T>(result);
        }
    }
}
