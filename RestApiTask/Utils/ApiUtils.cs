using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestApiTask.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using RestApiTask.Constants;
using Task4SmartDataDrivenKPC.Models;

namespace RestApiTask.Utils
{
    public class ApiUtils
    {
        private static readonly TestData testData = FileReader.ReadJsonData<TestData>(ProjectConstants.PathToTestData);

        private static string host = testData.Host; 

        public static HttpResponseMessage GetRequest(string request)
        {
            HttpClient client = new HttpClient();

            string uri = host + request; 

            HttpResponseMessage response = client.GetAsync(uri).Result;

            client.Dispose();

            return response;
        }
    }
}
