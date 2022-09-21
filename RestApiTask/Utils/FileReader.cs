using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApiTask.Constants;
using RestApiTask.Models;

namespace RestApiTask.Utils
{
    public static class FileReader
    {
        private static Logger Log = Logger.Instance;

        public static Dictionary<string, string> requestUrl = new Dictionary<string, string>();
        
        public static void GetRequestUrls()
        {
            Log.Info("Get request URLs");
            var filePath = ProjectConstants.PathToRequestData;
            var json = File.ReadAllText(filePath);
            var jsonObj = JObject.Parse(json);

            foreach (var element in jsonObj)
            {
                requestUrl.Add(element.Key, element.Value.ToString());
            }
        }

        public static void ClearLogFile()
        {
            FileInfo file = new FileInfo(ProjectConstants.PathToLogFile);

            if (file.Exists)
            {
                file.Delete();
                Log.Info("Log file deleted");
            }
        }

        public static T ReadJsonData<T>(string path)
        {
            Log.Info("Start deserializing");
            return JsonConvert.DeserializeObject<T>(ReadFile(path));
        }

        

        public static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                Log.Info("Start file reading");
                return sr.ReadToEnd();
            }
        }


        //public static List<T> DeserializeJsonToList<T>(string path)
        //{
        //    Log.Info("Start deserializing to list of objects");
        //    return JsonConvert.DeserializeObject<List<T>>(ReadFile(path));
        //}


        public static void GetRequestModel()
        {
            //var jsonObj = JObject.Parse(requestInfoJson);
            //var request = jsonObj["Request1"].ToString();

            //var objResponse1 =
            //    JsonConvert.DeserializeObject<List<RequestModel>>(request);

            //RequestModel requestModel = new RequestModel();

            //requestModel = objResponse1[0];
        }

        public static Dictionary<string, string> GetTestData(string setName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData.json");
            var json = File.ReadAllText(filePath);
            var jsonObj = JObject.Parse(json);

            var testDataObj = jsonObj[setName].ToString();

            var testData = JsonConvert.DeserializeObject<Dictionary<string, string>>(testDataObj);

            return testData;
        }

        public static string ReadJsonTestData()
        {
            string pathTestData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData.json");
            string testData = File.ReadAllText("TestData.json");

            return testData;
        }
    }
}