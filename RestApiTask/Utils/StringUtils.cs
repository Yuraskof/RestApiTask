using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTask.Utils
{
    public class StringUtils
    {
        public static string StringGenerator(int lettersCount)
        {
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz".ToCharArray();

            Random rand = new Random();

            string word = "";

            for (int j = 1; j <= lettersCount; j++)
            {
                int letter = rand.Next(0, letters.Length - 1);

                word += letters[letter];
            }
            //log.Info(string.Format("generated random text = {0}", word));
            return word;
        }

        static class DataSetter
        {
            //public static TestConfig testData;
            public static void SetTestData()
            {
                //testData = JsonConvert.DeserializeObject<TestConfig>(DataReader.ReadJsonTestData());
            }
        }

        public class ConfigModel
        {
            public string baseUrl { get; set; }
            public string browser { get; set; }
            public Regime[] regime { get; set; }
            public int wait_time { get; set; }
        }

        public class Regime
        {
            public string optionsChrome { get; set; }
            public string regimeFirefox { get; set; }
            public string languageFirefox { get; set; }
        }

        public static class DataReader
        {
            public static Dictionary<string, string> GetTestData(string setName)
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData.json");
                var json = File.ReadAllText(filePath);
                var jsonObj = JObject.Parse(json);

                var testDataObj = jsonObj[setName].ToString();

                var testData = JsonConvert.DeserializeObject<Dictionary<string, string>>(testDataObj);

                return testData;
            }


            public static void GetBrowserConfig()
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                var json = File.ReadAllText(filePath);
                var jsonObj = JObject.Parse(json);

                foreach (var element in jsonObj)
                {
                    //Driver.BrowserConfig.Add(element.Key, element.Value.ToString());
                }
            }
        }
    }
}
