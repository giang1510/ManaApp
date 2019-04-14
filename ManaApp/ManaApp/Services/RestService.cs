using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using ManaApp.Model;
using ManaApp.InterfaceCrossPlatform;

namespace ManaApp
{
    public class RestService : IRestService
    {
        HttpClient client;
        const string BASEURL = "http://192.168.0.10:3000/";

        //public List<TodoItem> Items { get; private set; }

        public RestService()
        {
            var authData = string.Format("{0}:{1}", "pass123", "123");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Digest", authHeaderValue);
        }

        public async Task<string> GetPage(string relativePath)
        {
            var url = BASEURL + relativePath;
            var uri = new Uri(string.Format(url, string.Empty));
            string result = "";

            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                result = ex.StackTrace;
            }

            return result;
        }

        public async Task<string> RefreshDataAsync()
        {
            var RestUrl = "http://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/198440/allinfo";
            var TestUrl = "http://192.168.0.10:3000/userJSON/login";
            var uri = new Uri(string.Format(TestUrl, string.Empty));

            string result = "";
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = uri;
                request.Method = HttpMethod.Post;
                string jsonData = @"{""username"" : ""pass123"", ""password"" : ""123""}";
                var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                    //VideoData obj = JsonConvert.DeserializeObject<VideoData>(content);
                    //var js = JObject.Parse(content);
                    //var category1 = new Category
                    //{
                    //    categoryName = "cName",
                    //    parentID = 3,
                    //    subjectID = 2,
                    //    categoryDescription = "description",
                    //    categoryID = 1,
                    //    forumCategoryID = 4,
                    //    videosCount = 10
                    //};

                    //result = obj.categoryName;


                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                result = ex.StackTrace;
            }
            return result;
        }

        public async Task<string> Login(string username, string password)
        {
            string jsonData = "{" + toJsonEntry("username", username) + ", " + toJsonEntry("password", password) + "}";
            var path = "userJSON/login";
            
            return await doPOST(path, jsonData);
        }

        //Do a Post-Request
        private async Task<string> doPOST(string relativePath, string jsonData)
        {
            var RestUrl = BASEURL + relativePath;
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            string result = "";
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = uri;
                request.Method = HttpMethod.Post;
                var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                result = ex.StackTrace;
            }
            return result;
        }

        private string toJsonEntry(string key, string value)
        {
            return quotedStr(key) + " : " + quotedStr(value);
        }

        private string quotedStr(string text)
        {
            return "\"" + text + "\"";
        }

        public async Task<string> Register(User user)
        {
            var path = "userJSON/register";
            var jsonData = userToJsonStr(user);

            return await doPOST(path, jsonData);
        }

        private string userToJsonStr(User user)
        {
            string usernameEntry = toJsonEntry(Constants.USER_USERNAME, user.username);
            string nameEntry = toJsonEntry(Constants.USER_NAME, user.name);
            string emailEntry = toJsonEntry(Constants.USER_EMAIL, user.email);
            string passwordEntry = toJsonEntry(Constants.USER_PASSWORD, user.password);
            string result = "{"
                + usernameEntry + ", "
                + nameEntry + ", "
                + emailEntry + ", "
                + passwordEntry + " }";
            return result;
        }
    }
}
