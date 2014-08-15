using PrismDemo.Tasks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Tasks.Services
{
    public class TaskService : ITaskService
    {
        public List<TaskViewModel> GetTasks()
        {
            var result = new List<TaskViewModel>();
            result.Add(new TaskViewModel()
            {
                Title = "Task 1",
                Description = "Description for task 1",
                Priority = 1
            });

            result.Add(new TaskViewModel()
            {
                Title = "Task 2",
                Description = "Description for task 2",
                Priority = 3
            });

            result.Add(new TaskViewModel()
            {
                Title = "Task 3",
                Description = "Description for task 3",
                Priority = 1
            });

            return result;
        }
    }
}
