using RestApiTask.Constants;
using Task4SmartDataDrivenKPC.Models;

namespace RestApiTask.Utils
{
    public class ApiUtils
    {
        private static Logger Log = Logger.Instance;

        private static readonly TestData testData = FileReader.ReadJsonData<TestData>(ProjectConstants.PathToTestData);

        private static string host = testData.Host; 

        public static HttpResponseMessage GetRequest(string request)
        {
            Log.Info("Get request");

            HttpClient client = new HttpClient();

            string uri = host + request; 

            HttpResponseMessage response = client.GetAsync(uri).Result;

            client.Dispose();

            return response;
        }
    }
}
