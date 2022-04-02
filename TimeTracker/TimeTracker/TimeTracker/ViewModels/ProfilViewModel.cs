using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Storm.Mvvm.Services;
using TimeTracker.Pages;
using TimeTracker.Services;
using TimeTracker.ThrowException;
using Xamarin.Forms;

namespace TimeTracker.ViewModels
{
    class ProfilViewModel : ViewModelBase
    {

        private ApiService _apiService = new ApiService();
        public string Email { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public bool Visible { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfilViewModel()
        {

        }
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, e);

        }
        public async void UserProfilCommand()
        {
            var accessToken = AccessToken;
            try
            {
                var isSuccess = await _apiService.UserProfilAsync(accessToken);
                if (isSuccess.IsSuccess)
                {
                    Email = isSuccess.Data.Email;
                    UserFirstName = isSuccess.Data.FirstName;
                    UserLastName = isSuccess.Data.LastName;
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
            }
            catch (WrongAccessTokenException e)
            {
                Console.WriteLine(e);
            }
        }
        public ICommand ChangeInfosCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await _apiService.ChangeProfilAsync(AccessToken, Email, UserFirstName, UserLastName);
                        Back();
                        await Application.Current.MainPage.Navigation.PushAsync(new MenuPage());
                    }
                    catch (WrongAccessTokenException e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }

        }
        public ICommand ChangePasswordCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await _apiService.ChangePasswordAsync(AccessToken, OldPassword, NewPassword);
                        Back();
                        await Application.Current.MainPage.Navigation.PushAsync(new MenuPage());
                    }
                    catch (WrongOldPasswordException e)
                    {
                        Message = "Wrong old password";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                        Console.WriteLine(e);
                    }
                });
            }

        }
        public ICommand ChangePasswordDisplay
        {
            get
            {
                return new Command(Visibilite);

            }
        }
        public void Visibilite()
        {
            Visible = !Visible;
        }
        public async void Back()
        {
            try
            {
                NavigationService Navserv = new NavigationService();
                await Navserv.PopAsync();
            }
            catch (Exception e)
            {

            }
        }
    }
}