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
            FileReader.GetRequestUrls();
        }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(FileReader.CheckIdAreAscending(FileReader.requestUrl["Request1"], StatusCodes.OK));
        }

    }
}