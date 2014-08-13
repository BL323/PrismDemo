using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismDemo.Graphing.Views;
using PrismDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Graphing
{
    public class GraphingModule : IModule
    {
        private IUnityContainer _container;
        private GraphingController _controller;

        public GraphingModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _controller = _container.Resolve<GraphingController>();

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.BodyRegion, typeof(LineGraphView));
        }
    }
}
