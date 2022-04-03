using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Projects;
using TimeTracker.Dtos.Util;
using TimeTracker.Services;
using TimeTracker.ThrowException;
using Xamarin.Forms;

namespace TimeTracker.ViewModels
{
    class TaskViewModel : ViewModelBase, INotifyPropertyChanged
    {
        ApiService _apiService = new ApiService();

        public string Name { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public long Id { get; set; }

        private ObservableCollection<Task> _tasks;

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskViewModel()
        {
            Tasks = new ObservableCollection<Task>();
            showData();
        }

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
                        var isSuccess = await _apiService.CreateTaskAsync(AccessToken, Name, Id);
                        Tasks.Clear();


                        if (isSuccess.IsSuccess)
                        {
                            Message = "Create Success!";
                            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                            showData();
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

        public async void showData()
        {
            Response<List<TaskItem>> TasksList = await ApiUtil.Instance.Api.TasksAsync(ApiUtil.Instance.Access_token, Id);
            if (TasksList.Data != null)
            {
                foreach (TaskItem _tasks in TasksList.Data)
                {
                    Task t = new Task(_tasks.Name, this, _tasks.Id);
                    Tasks.Add(t);
                }
            }

        }
    }
}