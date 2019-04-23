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
        const string LOGIN_PATH = "userJSON/login";
        const string SEARCH_PATH = "search_machine/JSON";

        HttpClient client;
        string baseURL = "http://192.168.0.13:3000/";

        //public List<TodoItem> Items { get; private set; }

        public RestService()
        {
            baseURL = "https://appointment-server.herokuapp.com/";

            var authData = string.Format("{0}:{1}", "pass123", "123");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Digest", authHeaderValue);
        }

        public async Task<string> GetPage(string relativePath)
        {
            var url = baseURL + relativePath;
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

        public async Task<LoginResponse> Login(User user)
        {
            var jsonData = JsonConvert.SerializeObject(user);
            var responseStr = await DoPOST(LOGIN_PATH, jsonData);
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseStr);
            return loginResponse;
        }

        //Do a Post-Request
        private async Task<string> DoPOST(string relativePath, string jsonData)
        {
            var RestUrl = baseURL + relativePath;
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

        public async Task<string> Register(User user)
        {
            var path = "userJSON/register";
            var jsonData = JsonConvert.SerializeObject(user);
            return await DoPOST(path, jsonData);
        }

        public async Task<string> SearchProvider(SearchInput searchInput)
        {
            var searchInputJson = JsonConvert.SerializeObject(searchInput);
            string searchResult = await DoPOST(SEARCH_PATH, searchInputJson);
            return searchResult;
        }
    }
}
