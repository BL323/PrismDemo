using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using PrismDemo.User.Services;
using PrismDemo.User.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.User
{
    public class UserModule : IModule
    {
        private IUnityContainer _container = null;
        private UserController _controller = null;

        public UserModule(IUnityContainer container)
        {
            _container = container;
            _controller = _container.Resolve<UserController>();
        }

        public void Initialize()
        {
            //Get current user and set as DataContext of view
            var regionManager = _container.Resolve<IRegionManager>();
            var region = regionManager.Regions[RegionNames.SidePannelRegion];

            var view = _container.Resolve<UserView>();

            var userService = _container.Resolve<IUserService>();
            var currentUser = userService.GetCurrentUser();


            foreach (var v in region.Views)
                region.Remove(v);

            region.Add(view);
            view.DataContext = currentUser;
            region.Activate(view);
        }
    }
}
