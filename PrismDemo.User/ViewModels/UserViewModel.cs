using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using PrismDemo.Infrastructure.Commands;
using PrismDemo.User.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrismDemo.User.ViewModels
{
    public class UserViewModel : UpdateBase
    {
        private int _yearOfStudy = 1;
        private double _id = 5d;
        private string _userName = string.Empty;
        private string _courseCode = string.Empty;
        private ICommand _userLogOutCommand;
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;

        public int YearOfStudy
        {
            get { return _yearOfStudy; }
            set
            {
                _yearOfStudy = value;
                RaisePropertyChanged(() => YearOfStudy);
            }
        }
        public double Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }

        }
        public string CourseCode
        {
            get { return _courseCode; }
            set
            {
                _courseCode = value;
                RaisePropertyChanged(() => CourseCode);
            }
        }
        public ICommand UserLogOutCommand
        {
            get { return _userLogOutCommand ?? (_userLogOutCommand = new RelayCommand(o => UserLogOut(), o => true)); }
        }

        public UserViewModel(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();          
        }

        private void UserLogOut()
        {
            _eventAggregator.GetEvent<UserLogOutEvent>().Publish(this);
        }
    }
}
