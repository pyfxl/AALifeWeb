using AALife.WebMvc.jqGrid;
using AALife.WebMvc.Models.Query;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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