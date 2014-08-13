using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using PrismDemo.Tasks.Events;
using PrismDemo.Tasks.ViewModels;
using PrismDemo.Tasks.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Tasks
{
    public class TasksController
    {
        private IUnityContainer _container = null;
        private IRegionManager _regionManager = null;
        private IEventAggregator _eventAggregator = null;
        private ILoggerFacade _loggerFacade = null;

        public TasksController(IUnityContainer container)
        {
            _container = container;

            _regionManager = _container.Resolve<IRegionManager>();
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _loggerFacade = _container.Resolve<ILoggerFacade>();

            _eventAggregator.GetEvent<ShowTaskEvent>().Subscribe(ShowCustomer,true);
        }

        private void ShowCustomer(TaskViewModel task)
        {
            var region = _regionManager.Regions[RegionNames.TaskRegion];
            var view = _container.Resolve<TaskListItemView>();

            foreach (var v in region.Views)
                region.Remove(v);

            region.Add(view);
            view.DataContext = task;
            region.Activate(view);
        }
    }
}
