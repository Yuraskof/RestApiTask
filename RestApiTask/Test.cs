using RestApiTask.Constants;
using RestApiTask.Models;
using RestApiTask.Utils;

namespace RestApiTask
{
    public class Tests
    {
        private static Logger Log = Logger.Instance;

        [SetUp]
        public void Setup()
        {
            FileReader.ClearLogFile();
            FileReader.GetRequestUrls();
        }

        [Test]
        public void Test1()
        {
            Log.Info("Test started");
            Assert.IsTrue(PostModel.CheckIdAreAscending(FileReader.requestUrl["AllPostsRequest"]), "Ids are not ascending");
            Assert.IsTrue(ApiApplicationRequest.CheckStatusCode(StatusCodes.OK), "Status code not as expected");
            Log.Info("Step 1 completed");

            PostModel postFromResponce = ApiApplicationRequest.GetSpecifiedPost(FileReader.requestUrl["Post99Request"]);
            PostModel postFromTestData = PostModel.SetModelFromTestData("ResponsePost99");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(postFromTestData.Id == postFromResponce.Id, "Models ids are not equal");
                Assert.IsTrue(postFromTestData.UserId == postFromResponce.UserId, "Models user ids are not equal");
                Assert.NotNull(postFromResponce.Title, "Model title is empty");
                Assert.NotNull(postFromResponce.Body, "Model body is empty");
            });
            Log.Info("Step 2 completed");

        }


    }
}