using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TimeTracker.Pages;
using TimeTracker.Services;
using Xamarin.Forms;

namespace TimeTracker.ViewModels
{
    class ProfilViewModel
    {

        private ApiService _apiService = new ApiService();
        public string Email { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }

        public ICommand UserProfilCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = AccessToken;
                    var isSuccess = await _apiService.UserProfilAsync(accessToken);

                    if (isSuccess.IsSuccess)
                    {
                        Email = "Your email is: " + isSuccess.Data.Email;
                        UserFirstName = "Your First name is: " + isSuccess.Data.FirstName;
                        UserLastName = "Your Last name is: " + isSuccess.Data.LastName;
                        await Application.Current.MainPage.Navigation.PushAsync(new ProfilPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    }
                });

            }

        }
    }
}