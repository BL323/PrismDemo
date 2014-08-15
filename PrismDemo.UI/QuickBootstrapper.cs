using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using PrismDemo.Graphing;
using PrismDemo.Presentation;
using PrismDemo.Tasks;
using PrismDemo.Tasks.Services;
using PrismDemo.User;
using PrismDemo.User.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismDemo.UI
{
    public class QuickBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new ShellWindow();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = base.CreateModuleCatalog();

            var presentationModule = typeof(PresentationModule);
            moduleCatalog.AddModule(new ModuleInfo() {ModuleName = presentationModule.Name, ModuleType = presentationModule.AssemblyQualifiedName });

            var graphingModule = typeof(GraphingModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = graphingModule.Name, ModuleType = graphingModule.AssemblyQualifiedName });

            var tasksModule = typeof(TasksModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = tasksModule.Name, ModuleType = tasksModule.AssemblyQualifiedName });

            var userModule = typeof(UserModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = userModule.Name, ModuleType = userModule.AssemblyQualifiedName });

            return moduleCatalog;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Register services
            RegisterTypeIfMissing(typeof(ITaskService), typeof(TaskService), true);
            RegisterTypeIfMissing(typeof(IUserService), typeof(UserService), true);
        }
    }
}
