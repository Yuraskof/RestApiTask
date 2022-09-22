using RestApiTask.Constants;
using RestApiTask.Models;
using RestApiTask.Utils;
using Task4SmartDataDrivenKPC.Models;

namespace RestApiTask
{
    public class Test
    {
        public static Logger Log = Logger.Instance;
        public static readonly TestData testData = FileReader.ReadJsonData<TestData>(ProjectConstants.PathToTestData);

        [SetUp]
        public void Setup()
        {
            FileReader.ClearLogFile();
            FileReader.GetRequestUrls();
        }

        [Test]
        public void TestApi()
        {
            Log.Info("Test started");
            List<PostModel> postModels = ApiApplicationRequest.GetAllPosts();
            Assert.IsTrue(ModelUtils.CheckIdAreAscending(postModels), "Ids are not ascending");
            Assert.IsTrue(ApiApplicationRequest.CheckStatusCode(StatusCodes.OK), "Status code not as expected");
            Log.Info("Step 1 completed");

            PostModel post99modelFromResponse = ApiApplicationRequest.GetSpecifiedPost(Convert.ToInt32(FileReader.requestUrlNumbers["Post99"]));
            PostModel post99FromTestData = ModelUtils.SetPostModelFromTestData("ResponseGetPost99");
            Assert.IsTrue(ApiApplicationRequest.CheckStatusCode(StatusCodes.OK), "Status code not as expected");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(post99FromTestData.Id == post99modelFromResponse.Id, "Models ids are not equal");
                Assert.IsTrue(post99FromTestData.UserId == post99modelFromResponse.UserId, "Models user ids are not equal");
                Assert.NotNull(post99modelFromResponse.Title, "Model title is empty");
                Assert.NotNull(post99modelFromResponse.Body, "Model body is empty");
            });
            Log.Info("Step 2 completed");

            PostModel post150modelFromResponse = ApiApplicationRequest.GetSpecifiedPost(Convert.ToInt32(FileReader.requestUrlNumbers["Post150"]));
            Assert.IsTrue(ApiApplicationRequest.CheckStatusCode(StatusCodes.NotFound), "Status code not as expected");
            Assert.Multiple(() =>
            {
                Assert.IsNull(post150modelFromResponse.Id, "Models id is not empty");
                Assert.IsNull(post150modelFromResponse.UserId, "Models user id is not empty");
                Assert.IsNull(post150modelFromResponse.Title, "Model title is not empty");
                Assert.IsNull(post150modelFromResponse.Body, "Model body is not empty");
            });
            Log.Info("Step 3 completed");

            PostModel modelToPostFromTestData = ModelUtils.SetPostModelFromTestData("ModelToPost");
            modelToPostFromTestData.Body = StringUtils.StringGenerator(Convert.ToInt32(testData.LettersCount));
            modelToPostFromTestData.Title = StringUtils.StringGenerator(Convert.ToInt32(testData.LettersCount));

            PostModel postModelCreatedPost = ApiApplicationRequest.CreatePost(modelToPostFromTestData);
            Assert.IsTrue(ApiApplicationRequest.CheckStatusCode(StatusCodes.Created), "Status code not as expected");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(modelToPostFromTestData.UserId == postModelCreatedPost.UserId, "Models user ids are not equal");
                Assert.IsTrue(modelToPostFromTestData.Body == postModelCreatedPost.Body, "Models  bodies are not equal");
                Assert.IsTrue(modelToPostFromTestData.Title == postModelCreatedPost.Title, "Model titles are not equal");
                Assert.IsNotEmpty(postModelCreatedPost.Id, "Model id is empty");
            });
            Log.Info("Step 4 completed");

            UserModel userModelFromTestData = FileReader.ReadJsonData<UserModel>(ProjectConstants.PathToUserModel);
            List<UserModel> userModels = ApiApplicationRequest.GetAllUsers();
            UserModel userModelFromResponse = ModelUtils.GetModelById(userModels, Convert.ToInt32(testData.UserId));
            Assert.IsTrue(ApiApplicationRequest.CheckStatusCode(StatusCodes.OK), "Status code not as expected");
            Assert.IsTrue(userModelFromTestData.Equals(userModelFromResponse), "Models are not equal");
            Log.Info("Step 5 completed");

            UserModel user5modelFromResponse = ApiApplicationRequest.GetSpecifiedUser(Convert.ToInt32(FileReader.requestUrlNumbers["User5"]));
            Assert.IsTrue(ApiApplicationRequest.CheckStatusCode(StatusCodes.OK), "Status code not as expected");
            Assert.IsTrue(userModelFromTestData.Equals(user5modelFromResponse), "Models are not equal");
            Log.Info("Step 6 completed. Test case successfully completed.");
        }
    }
}