using Microsoft.Practices.Prism.PubSubEvents;
using PrismDemo.Graphing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Graphing.Events
{
    public class PerturbationsUpdatedEvent : PubSubEvent<LinePerturbationViewModel>
    {
    }
}
