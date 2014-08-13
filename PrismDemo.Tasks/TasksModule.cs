using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using PrismDemo.Tasks.ViewModels;
using PrismDemo.Tasks.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Tasks
{
    public class TasksModule : IModule
    {
        private IUnityContainer _container = null;
        private TasksController _controller = null;

        public TasksModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _controller = _container.Resolve<TasksController>();

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.BodyRegion, typeof(TaskShellView));
            regionManager.RegisterViewWithRegion(RegionNames.TaskListRegion, typeof(TasksListView));
        }
    }
}
