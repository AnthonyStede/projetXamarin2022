using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Dtos.Authentications;
using Xamarin.Essentials;

namespace TimeTracker.Services
{
    class ApiUtil
    {
        public static ApiUtil Instance = new ApiUtil();

        public ApiService Api = new ApiService();
        protected string access_token; 
        public string Access_token 
        {
            get => access_token;
        }

        public void loginInfo(LoginResponse response)
        {
            access_token = response.AccessToken;
            Preferences.Set("refresh_token", response.RefreshToken);
            Preferences.Set("expires_in", response.ExpiresIn);
        }

    }
}
