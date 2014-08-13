using PrismDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Presentation.ViewModels
{
    public class HeaderViewModel : UpdateBase
    {
        private string _versionNumber = "1.00.04.01";
        public string VersionNumber
        {
            get { return _versionNumber; }
            set
            {
                if (_versionNumber != value)
                {
                    _versionNumber = value;
                }
            }
        }
    }
}
