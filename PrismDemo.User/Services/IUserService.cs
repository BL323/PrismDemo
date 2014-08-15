using PrismDemo.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.User.Services
{
    public interface IUserService
    {
        UserViewModel GetCurrentUser();
    }
}
