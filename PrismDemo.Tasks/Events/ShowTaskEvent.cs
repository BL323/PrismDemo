using Microsoft.Practices.Prism.PubSubEvents;
using PrismDemo.Tasks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Tasks.Events
{
    public class ShowTaskEvent : PubSubEvent<TaskViewModel>
    {
    }
}
