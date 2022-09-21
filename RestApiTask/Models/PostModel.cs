﻿using Newtonsoft.Json;
using RestApiTask.Utils;
using Newtonsoft.Json.Linq;
using RestApiTask.Constants;

namespace RestApiTask.Models
{
    public class PostModel
    {
        public string UserId { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }


        private static Logger Log = Logger.Instance;


        public static PostModel SetModelFromTestData(string arrayName)
        {
            Log.Info("Start deserializing");
            
            var jsonObj = JObject.Parse(FileReader.ReadFile(ProjectConstants.PathToPostsModels));
            var jsonModel = jsonObj[arrayName].ToString();

            var objModels = JsonConvert.DeserializeObject<List<PostModel>>(jsonModel);

            PostModel postModel = new PostModel();
            postModel = objModels[0];
            
            return postModel;
        }

        public static bool CheckIdAreAscending(string responseUrl)
        {
            Log.Info("Check Ids are ascending");

            List<PostModel> postModels = ApiApplicationRequest.GetAllPosts(responseUrl);

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
    }
}
