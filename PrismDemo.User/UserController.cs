using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using PrismDemo.User.Events;
using PrismDemo.User.ViewModels;
using PrismDemo.User.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.User
{
    public class UserController
    {
        private IUnityContainer _container = null;
        private IRegionManager _regionManager = null;
        private IEventAggregator _eventAggregator = null;

        public UserController(IUnityContainer container)
        {
            _container = container;
            _regionManager = _container.Resolve<IRegionManager>();
            _eventAggregator = _container.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<UserLogOutEvent>().Subscribe(UserLogOut, true);
        }

        private void UserLogOut(UserViewModel viewModel)
        {
            //Fires this method
            var region = _regionManager.Regions[RegionNames.SidePannelRegion];
            var view = _container.Resolve<LogInView>();

            foreach (var v in region.Views)
                region.Remove(v);

            region.Add(view);
            region.Activate(view);
        }
    }
}
