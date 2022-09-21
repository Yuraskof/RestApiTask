using Newtonsoft.Json;
using RestApiTask.Models;

namespace RestApiTask.Utils
{
    public class ApiApplicationRequest
    {
        private static Logger Log = Logger.Instance;

        public static HttpResponseMessage response;

        public static List<PostModel> GetAllPosts(string responseUrl)
        {
            Log.Info("Get all posts");
            response = ApiUtils.GetRequest(responseUrl);

            string contentString = response.Content.ReadAsStringAsync().Result;

            var postModels = JsonConvert.DeserializeObject<List<PostModel>>(contentString);
            return postModels;
        }

        public static PostModel GetSpecifiedPost(string responseUrl)
        {
            Log.Info(string.Format("Get {0} post", responseUrl));
            response = ApiUtils.GetRequest(responseUrl);

            string contentString = response.Content.ReadAsStringAsync().Result;

            var postModel = JsonConvert.DeserializeObject<PostModel>(contentString);
            return postModel;
        }

        public static bool CheckStatusCode(int expectedStatusCode)
        {
            Log.Info("Check status code");
            int statusCodeValue = (int)response.StatusCode;
            return statusCodeValue == expectedStatusCode;
        }
    }
}
