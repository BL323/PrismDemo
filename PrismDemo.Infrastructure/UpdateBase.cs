using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Infrastructure
{
    public abstract class UpdateBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
                return;

            var memExpression = expression.Body as MemberExpression;
            if (memExpression != null)
            {
                OnPropertyChanged(memExpression.Member.Name);
            }
        }
    }
}
