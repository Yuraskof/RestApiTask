using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Internal;
using RestApiTask.Constants;
using RestApiTask.Models;

namespace RestApiTask.Utils
{
    public static class FileReader
    {
        public static Dictionary<string, string> requestModel = new Dictionary<string, string>();
        public static Dictionary<string, string> ProductInfo = new Dictionary<string, string>();

        public static string requestInfoJson = ReadFile(ProjectConstants.PathToRequestData);



        public static bool CheckIdAreAscending(string response)
        {
            var contentString = ApiUtils.Search(response);

            dynamic parsedJson = JsonConvert.DeserializeObject(contentString);

            int previousId = -1;

            foreach (var element in parsedJson)
            {
                var post = element.ToString();

                PostModel postModel = new PostModel();

                postModel = JsonConvert.DeserializeObject<PostModel>(post);

                int id = Convert.ToInt32(postModel.Id);

                if (previousId < 0)
                {
                    previousId = id;
                    continue;
                }

                if (id < previousId)
                {
                    return false;
                }
                else
                {
                    previousId = id;
                }
            }
            return true;
        }

        public static void GetRequestModel()
        {
            var jsonObj = JObject.Parse(requestInfoJson);
            var request = jsonObj["Request1"].ToString();

            var objResponse1 =
                JsonConvert.DeserializeObject<List<RequestModel>>(request);

            RequestModel requestModel = new RequestModel();

            requestModel = objResponse1[0];

            //foreach (var element in parsedJson)
            //{
            //    var request = element["Request1"].ToString();
            //    //var request = element.ToString();

            //    RequestModel requestModel = new RequestModel();

            //    requestModel = JsonConvert.DeserializeObject<RequestModel>(request);

            //    var path = element[1].ToString();
            //    int id = Convert.ToInt32(requestModel.Path);

            //}
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


        public static void GetTestData()
        {
            var filePath = ProjectConstants.PathToRequestData;
            var json = File.ReadAllText(filePath);
            var jsonObj = JObject.Parse(json);

            foreach (var element in jsonObj)
            {
                requestModel.Add(element.Key, element.Value.ToString());
            }
        }

        public static void GetRequestInfo(string key)
        {
            string allUserInfo = requestModel[key];
            string[] separatedData = allUserInfo.Split("\t");

            List<string> productInfoFields = new List<string>()
                { "OperatingSystem", "ProductName"};

            ProductInfo.Clear();

            for (int i = 0; i < separatedData.Length; i++)
            {
                ProductInfo.Add(productInfoFields[i], separatedData[i]);
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
            return JsonConvert.DeserializeObject<T>((path)); //ReadFile
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