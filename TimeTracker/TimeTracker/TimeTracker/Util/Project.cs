using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using TimeTracker.Dtos.Projects;
using TimeTracker.ViewModels;

namespace TimeTracker.Dtos.Util
{
    class Project: NotifierBase
    {
        private string name;
        private string description;
        public readonly ProjectViewModel ProjectViewModel;
        private long projectId;
 
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public long Id
        {
            get => projectId;
            set => SetProperty(ref projectId, value);
        }

        public ICommand DeleteCommand { get; }

        public ICommand EditCommand { get; }

        public Project(ProjectViewModel projectViewModel, Response<ProjectItem> response) : this(
            response.Data.Name, projectViewModel, response.Data.Id)
        {

        }

        public Project(string name, ProjectViewModel projectViewModel, long projectId)
        {
            Name = name;
            ProjectViewModel = projectViewModel;
            Id = projectId;
        }

    }
}
