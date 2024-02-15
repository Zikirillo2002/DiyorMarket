using Lesson11.ViewModels;
using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.User
{
    public interface IUserDataStore
    {
        public (bool, string) AuthenticateLogin(UserLogin user);
        public (bool, string) RegisterLogin(UserLogin user);
    }
}
