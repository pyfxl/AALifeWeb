using Newtonsoft.Json.Converters;

namespace AALife.WebMvc
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T query)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(query, timeConverter);
        }
    }
}