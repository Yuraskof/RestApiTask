using RestApiTask.Models;

namespace RestApiTask.Utils
{
    public class ApiApplicationRequest
    {
        public static HttpResponseMessage response;
        private const string postsPath = "/posts/";
        private const string usersPath = "/users/";

        public static List<PostModel> GetAllPosts()
        {
            Test.Log.Info("Get all posts");
            response = ApiUtils.GetRequest(postsPath);

            string contentString = response.Content.ReadAsStringAsync().Result;
            
            return JsonUtils.ReadJsonData<List<PostModel>>(contentString);
        }

        public static PostModel GetSpecifiedPost(int number)
        {
            Test.Log.Info(string.Format("Get {0} post", number));
            response = ApiUtils.GetRequest(postsPath+number);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<PostModel>(contentString);
        }

        public static List<UserModel> GetAllUsers()
        {
            Test.Log.Info("Get all users");
            response = ApiUtils.GetRequest(usersPath);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<List<UserModel>>(contentString);
        }

        public static UserModel GetSpecifiedUser(int number)
        {
            Test.Log.Info(string.Format("Get {0} user", number));
            response = ApiUtils.GetRequest(usersPath+number);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<UserModel>(contentString);
        }

        public static PostModel CreatePost(object content)
        {
            Test.Log.Info("Send post");
            response = ApiUtils.PostRequest(postsPath, content);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<PostModel>(contentString);
        }

        public static bool CheckStatusCode(int expectedStatusCode)
        {
            Test.Log.Info("Check status code");
            int statusCodeValue = (int)response.StatusCode;
            return statusCodeValue == expectedStatusCode;
        }
    }
}
