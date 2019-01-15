using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AALife.WebMvc
{
    public class JsonLowercase : IHttpActionResult
    {
        object _value;
        HttpRequestMessage _request;

        public JsonLowercase(object value, HttpRequestMessage request)
        {
            _value = value;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            json.ContractResolver = new LowercaseContractResolver();

            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent(typeof(object), _value, formatter),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }

    public class LowercaseContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}