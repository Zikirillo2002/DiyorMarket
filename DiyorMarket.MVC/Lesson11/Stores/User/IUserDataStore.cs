using Lesson11.ViewModels;
using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.User
{
    public interface IUserDataStore
    {
        public bool AuthenticateLogin(UserLogin user);
        public bool RegisterLogin(UserLogin user);
    }
}
