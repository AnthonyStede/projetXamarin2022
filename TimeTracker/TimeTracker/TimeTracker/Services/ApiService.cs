using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Diagnostics;
using TimeTracker.Dtos.Accounts;
using TimeTracker.Dtos.Authentications;
using TimeTracker.Dtos.Authentications.Credentials;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Projects;
using System.Collections.Generic;

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

        public async Task<Response<LoginResponse>> LoginAsync(string login, string password)
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

            Response<LoginResponse> responseBody = JsonConvert.DeserializeObject<Response<LoginResponse>>(jsonResponse);

            return responseBody;
        }

        public async Task<Response<UserProfileResponse>> UserProfilAsync(String AccessToken)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);

            var response = await client.GetAsync(Urls.USER_PROFIL);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Response<UserProfileResponse>>(content);

        }
        public async Task<Response<UserProfileResponse>> ChangeProfilAsync(string Accesstoken, string email, string userFirstName, string userLastName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Accesstoken);

            var ProfilModel = new UserProfileResponse()
            {
                Email = email,
                FirstName = userFirstName,
                LastName = userLastName
            };

            var json = JsonConvert.SerializeObject(ProfilModel);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(Urls.USER_PROFIL, content); // à changer par des patch

            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Response<UserProfileResponse>>(responseBody);
        }
        public async Task<Response<UserProfileResponse>> ChangePasswordAsync(string Accesstoken, string oldPassword, string newPassword)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Accesstoken);

            var ProfilModel = new SetPasswordRequest()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
            };

            var json = JsonConvert.SerializeObject(ProfilModel);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(Urls.SET_PASSWORD, content); // à changer par des patch

            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Response<UserProfileResponse>>(responseBody);
        }

        public async Task<Response<ProjectItem>> CreateProjectAsync(string AccessToken, string name, string description)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);

            var model = new AddProjectRequest
            {
                Name = name,
                Description = description
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Urls.PROJECT_LIST, content);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            Response<ProjectItem> responseBody = JsonConvert.DeserializeObject<Response<ProjectItem>>(jsonResponse);

            return responseBody;
        }

        public async Task<Response<List<ProjectItem>>> ProjetsAsync(string access_token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Urls.API);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);

            HttpResponseMessage response = await client.GetAsync(Urls.PROJECT_LIST);

            var result = await response.Content.ReadAsStringAsync();
            Response<List<ProjectItem>> responseBody = JsonConvert.DeserializeObject<Response<List<ProjectItem>>>(result);

            return responseBody;
        }
    }
}