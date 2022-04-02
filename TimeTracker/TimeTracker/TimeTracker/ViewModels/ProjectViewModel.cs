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
    class ProjectViewModel : ViewModelBase, INotifyPropertyChanged
    {
        ApiService _apiService = new ApiService();

        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }

        private ObservableCollection<Project> proJects;

        public ObservableCollection<Project> projects
        {
            get => proJects;
            set => SetProperty(ref proJects, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectViewModel()
        {
            projects = new ObservableCollection<Project>();
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
                        var isSuccess = await _apiService.CreateProjectAsync(AccessToken, Name,Description);
                        projects.Clear();


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
            Response<List<ProjectItem>> Projects = await ApiUtil.Instance.Api.ProjetsAsync(ApiUtil.Instance.Access_token);
            if (Projects.Data != null)
            {
                foreach (ProjectItem _projets in Projects.Data)
                {
                    Project p = new Project(_projets.Name, this, _projets.Id);
                    projects.Add(p);
                }
            }
         
        }
    }
}