using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Diagnostics;
using TimeTracker.Dtos.Accounts;
using TimeTracker.Dtos.Authentications.Credentials;
using TimeTracker.Dtos;

namespace TimeTracker.Services
{
    public interface IApiService
    {
        Task<TResponse> Get<TResponse>(string url);
    }
    public interface ILoginCommandParameter
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }

    public class CreateUserCommandParameter : ILoginCommandParameter
    {
        public CreateUserRequest Data { get; set; }
        public string ClientId => Data.ClientId;
        public string ClientSecret => Data.ClientSecret;
    }

    public class ApiService : IApiService
    {
        private const string HOST = "https://timetracker.julienmialon.ovh/";
        private readonly HttpClient _client = new HttpClient();

        public async Task<TResponse> Get<TResponse>(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, HOST + url);

            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }



        public async Task<ResponseBody> RegisterAsync(string email, string password, string userFirstName, string userLastName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);

            var model = new CreateUserRequest
            {
                ClientId = "MOBILE",
                ClientSecret = "COURS",
                Email = email,
                Password = password,
                FirstName = userFirstName,
                LastName = userLastName,
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Urls.REGISTER, content);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            ResponseBody responseBody = JsonConvert.DeserializeObject<ResponseBody>(jsonResponse);

            return responseBody;
        }

        public async Task<ResponseBody> LoginAsync(string login, string password)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);

            var LoginModel = new LoginWithCredentialsRequest
            {
                Login = login,
                Password = password,
                ClientId = "MOBILE",
                ClientSecret = "COURS"
            };
            var json = JsonConvert.SerializeObject(LoginModel);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Urls.LOGIN, content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);

            ResponseBody responseBody = JsonConvert.DeserializeObject<ResponseBody>(jsonResponse);

            return responseBody;
        }

        public async Task<Response<UserProfileResponse>> UserProfilAsync(String AccessToken)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);

            var response = await client.GetAsync(Urls.USER_PROFILE);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Response<UserProfileResponse>>(content);

        }
    }
}