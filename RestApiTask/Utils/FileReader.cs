using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApiTask.Constants;
using RestApiTask.Models;

namespace RestApiTask.Utils
{
    public static class FileReader
    {
        public static Dictionary<string, string> requestUrl = new Dictionary<string, string>();

        public static bool CheckStatusCode(HttpResponseMessage response, int statusCode)
        {
            int statusCodeValue = (int)response.StatusCode;

            return statusCodeValue == statusCode;
        }

        public static bool CheckIdAreAscending(string responseUrl, int statusCode)
        {
            var response = ApiUtils.GetRequest(responseUrl);

            if (!CheckStatusCode(response, statusCode))
            {
                return false;
            }

            string contentString = response.Content.ReadAsStringAsync().Result;

            var postModels = JsonConvert.DeserializeObject<List<PostModel>>(contentString); 

            int previousId = -1;

            foreach (var element in postModels)
            {
                int id = Convert.ToInt32(element.Id);

                if (previousId < 0)
                {
                    previousId = id;
                    continue;
                }

                if (id < previousId)
                {
                    return false;
                }
                
                previousId = id;
                
            }
            return true;
        }

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


        public static void GetRequestUrls()
        {
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
            }
        }
        
        public static T ReadJsonData<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(ReadFile(path)); 
        }

        private static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}