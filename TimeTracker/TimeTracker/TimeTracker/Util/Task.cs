using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using TimeTracker.Dtos.Projects;
using TimeTracker.ViewModels;

namespace TimeTracker.Dtos.Util
{
    class Task : NotifierBase
    {
        private string name;
        public readonly TaskViewModel TaskViewModel;
        private long taskId;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public long Id
        {
            get => taskId;
            set => SetProperty(ref taskId, value);
        }

        public ICommand DeleteCommand { get; }

        public ICommand EditCommand { get; }

        public Task(TaskViewModel taskViewModel, Response<TaskItem> response) : this(
            response.Data.Name, taskViewModel, response.Data.Id)
        {

        }

        public Task(string name, TaskViewModel taskViewModel, long projectId)
        {
            Name = name;
            TaskViewModel = taskViewModel;
            Id = projectId;
        }

    }
}
