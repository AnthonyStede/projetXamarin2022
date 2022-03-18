using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TimeTracker.Services;
using Xamarin.Forms;

namespace TimeTracker.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        ApiService _apiService = new ApiService();

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, e);
        }

        public ICommand RegisterCommand
        {

            get
            {
                return new Command(async () =>
                {

                    var isSuccess = await _apiService.RegisterAsync(Email, Password, UserFirstName, UserLastName);

                    if (isSuccess.IsSuccess)
                    {
                        Message = "Register successfully";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    }


                    else
                    {
                        Message = "Register failed, Retry please";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    }

                });
            }

        }


    }
}