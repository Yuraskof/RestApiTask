using RestApiTask.Constants;
using RestApiTask.Models;
using RestApiTask.Utils;
using Task4SmartDataDrivenKPC.Models;

namespace RestApiTask
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            FileReader.GetRequestModel();
            //Assert.IsTrue(FileReader.CheckIdAreAscending(requestModel.Path));
        }

    }
}