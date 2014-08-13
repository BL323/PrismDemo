using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using PrismDemo.Presentation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Presentation
{
    public class PresentationModule : IModule
    {
        private IUnityContainer _container;
        private PresentationController _controller;

        public PresentationModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _controller = _container.Resolve<PresentationController>();

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
        }
    }
}
