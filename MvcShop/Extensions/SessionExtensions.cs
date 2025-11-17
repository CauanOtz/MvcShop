using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MvcShop.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var s = session.GetString(key);
            return s == null ? default(T) : JsonSerializer.Deserialize<T>(s);
        }
    }
}
