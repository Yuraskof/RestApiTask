using Newtonsoft.Json;
using RestApiTask.Constants;
using System.Text;

namespace RestApiTask.Utils
{
    public class ApiUtils
    {
        private static string host = Test.testData.Host; 

        public static HttpResponseMessage GetRequest(string request)
        {
            Test.Log.Info(string.Format("Get request {1}{0}", request, host));

            HttpClient client = new HttpClient();

            string uri = host + request; 

            HttpResponseMessage response = client.GetAsync(uri).Result;

            client.Dispose();

            return response;
        }

        public static HttpResponseMessage PostRequest(string request, object content)
        {
            Test.Log.Info(string.Format("Post request {1}{0}", request, host));

            var stringContent = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(stringContent, Encoding.UTF8, ProjectConstants.MediaType);

            HttpClient client = new HttpClient();

            string uri = host + request;

            HttpResponseMessage response = client.PostAsync(uri, httpContent).Result;

            client.Dispose();

            return response;
        }
    }
}
