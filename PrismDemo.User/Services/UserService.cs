using Microsoft.Practices.Unity;
using PrismDemo.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.User.Services
{
    public class UserService : IUserService
    {
        private IUnityContainer _container = null;
        public UserService(IUnityContainer container)
        {
            _container = container;
        }

        public UserViewModel GetCurrentUser()
        {
            return new UserViewModel(_container)
            {
                UserName = "b.lourence",
                YearOfStudy = 4,
                CourseCode = "ComSci",
                Id = 1111753
            };
        }
    }
}
