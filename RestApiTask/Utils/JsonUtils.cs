using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestApiTask.Utils
{
    public static class JsonUtils
    {
        public static JObject ParseToJsonObject(string content)
        {
            Test.Log.Info("Start parsing to json object");
            return JObject.Parse(content);
        }

        public static T ReadJsonData<T>(string content)
        {
            Test.Log.Info("Start deserializing");
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
