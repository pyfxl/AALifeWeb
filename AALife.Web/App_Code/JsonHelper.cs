using Newtonsoft.Json;
using System.IO;

public class JsonHelper
{
    private static JsonSerializerSettings _jsonSettings = null;

    static JsonHelper()
    {
    }

    public static string JsonSerializer<T>(T t)
    {
        return JsonConvert.SerializeObject(t, Formatting.None, _jsonSettings);
    }

    public static T JsonDeserialize<T>(string jsonString)
    {
        return JsonConvert.DeserializeObject<T>(jsonString, _jsonSettings);
    }

}