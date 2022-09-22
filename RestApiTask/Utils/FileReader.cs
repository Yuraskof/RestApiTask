using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApiTask.Constants;

namespace RestApiTask.Utils
{
    public static class FileReader
    {
        public static Dictionary<string, string> requestUrlNumbers = new Dictionary<string, string>();
        
        public static void GetRequestUrls()
        {
            Test.Log.Info("Get request URLs");
            var filePath = ProjectConstants.PathToRequestData;
            var json = File.ReadAllText(filePath);
            var jsonObj = JObject.Parse(json);

            foreach (var element in jsonObj)
            {
                requestUrlNumbers.Add(element.Key, element.Value.ToString());
            }
        }

        public static void ClearLogFile()
        {
            FileInfo file = new FileInfo(ProjectConstants.PathToLogFile);

            if (file.Exists)
            {
                file.Delete();
                Test.Log.Info("Log file deleted");
            }
        }

        public static T ReadJsonData<T>(string path)
        {
            Test.Log.Info("Start deserializing");
            return JsonConvert.DeserializeObject<T>(ReadFile(path));
        }

        public static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                Test.Log.Info("Start file reading");
                return sr.ReadToEnd();
            }
        }
    }
}