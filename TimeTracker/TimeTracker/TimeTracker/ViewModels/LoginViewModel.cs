using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using TimeTracker.Pages;
using TimeTracker.Services;


namespace TimeTracker.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private ApiService _apiService = new ApiService();
        public string Login { get; set; }

        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, e);

        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSuccess = await _apiService.LoginAsync(Login, Password);

                    if (isSuccess.IsSuccess)
                    {
                        Message = "Login Success!";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                        await Application.Current.MainPage.Navigation.PushAsync(new ProjectListPage());
                        AccessToken = isSuccess.Data.AccessToken;
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(AccessToken)));
                        Messenger.Default.Send(AccessToken);
                        var param = new Dictionary<string, string>
                        {
                            {"AccessToken",AccessToken }
                        };
                    }
                    else
                    {
                        Message = "Login failed, Retry please";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    }
                });
            }

        }
    }
}