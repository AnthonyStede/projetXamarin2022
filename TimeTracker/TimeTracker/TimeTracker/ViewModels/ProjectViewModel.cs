using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TimeTracker.Services;
using TimeTracker.ThrowException;
using Xamarin.Forms;

namespace TimeTracker.ViewModels
{
    class ProjectViewModel : INotifyPropertyChanged
    {
        ApiService _apiService = new ApiService();

        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, e);
        }

        public ICommand CreateProjectCommand
        {
            get
            {
                return new Command(async () =>
                {

                    try
                    {
                        AccessToken = ApiUtil.Instance.Access_token;
                        var isSuccess = await _apiService.CreateProjectAsync(AccessToken, Name,Description);

                        if (isSuccess.IsSuccess)
                        {
                            Message = "Create Success!";
                            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                        }
                        else
                        {
                            Message = isSuccess.ErrorCode;
                            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                        }
                    }
                    catch (UserNotFoundException e)
                    {
                        Message = "Error";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    }


                });
            }

        }


    }
}