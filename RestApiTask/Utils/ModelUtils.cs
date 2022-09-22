using RestApiTask.Constants;
using RestApiTask.Models;

namespace RestApiTask.Utils
{
    public class ModelUtils
    {
        public static UserModel GetModelById(List<UserModel> models, int idNumber)
        {
            foreach (var element in models)
            {
                if (element.id == idNumber)
                {
                    Test.Log.Info("User model found");
                    return element;
                }
            }
            Test.Log.Info("User model not found");
            return null;
        }

        public static bool CheckIdAreAscending(List<PostModel> postModels)
        {
            Test.Log.Info("Check Ids are ascending");

            int previousId = -1;

            foreach (var element in postModels)
            {
                int id = Convert.ToInt32(element.Id);

                if (previousId < 0)
                {
                    previousId = id;
                    continue;
                }

                if (id < previousId)
                {
                    return false;
                }
                previousId = id;
            }
            return true;
        }

        public static PostModel SetPostModelFromTestData(string arrayName)
        {
            Test.Log.Info("Start creating a post model");

            var jsonObj = JsonUtils.ParseToJsonObject(FileReader.ReadFile(ProjectConstants.PathToPostsModels));
            var jsonModel = jsonObj[arrayName].ToString();

            var objModels = JsonUtils.ReadJsonData<List<PostModel>>(jsonModel);

            PostModel postModel = new PostModel();
            postModel = objModels[0];

            return postModel;
        }
    }
}
