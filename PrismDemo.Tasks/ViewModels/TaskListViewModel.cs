using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using PrismDemo.Tasks.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Tasks.ViewModels
{
    public class TaskListViewModel : UpdateBase
    {
        private IUnityContainer _container = null;
        private IEventAggregator _eventAggregator = null;

        private TaskViewModel _selectedTask = null;
        private ObservableCollection<TaskViewModel> _tasks = new ObservableCollection<TaskViewModel>();

        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    RaisePropertyChanged(() => SelectedTask);
                    UpdateTask();
                }
            }
        }

 
        public ObservableCollection<TaskViewModel> Tasks
        {
            get { return _tasks; }
            set
            {
                if (_tasks != value)
                {
                    _tasks = value;
                    RaisePropertyChanged(() => Tasks);
                }
            }
        }

        public TaskListViewModel(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();

            AddTask();
        }

        #region Private Methods
        private void AddTask()
        {
            Tasks.Add(new TaskViewModel()
            {
                Title = "Task 1",
                Description = "Description for task 1",
                Priority = 1
            });

            Tasks.Add(new TaskViewModel()
            {
                Title = "Task 2",
                Description = "Description for task 2",
                Priority = 3
            });

            Tasks.Add(new TaskViewModel()
            {
                Title = "Task 3",
                Description = "Description for task 3",
                Priority = 1
            });
        }
        private void UpdateTask()
        {
            _eventAggregator.GetEvent<ShowTaskEvent>().Publish(_selectedTask);
        }
        #endregion

    }
}
