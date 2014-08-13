using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using PrismDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Tasks.ViewModels
{
    public class TaskViewModel : UpdateBase
    {
        #region Private Fields
        private int _priority;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private Guid _id = new Guid();
        #endregion

        #region Public Properties
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }
        public int Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                RaisePropertyChanged(() => Priority);
            }
        }
        public Guid Id
        {
            get { return _id; }
        }
        #endregion
    }
}
