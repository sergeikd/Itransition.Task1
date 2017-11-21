using System.Collections.Generic;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Interfaces
{
    public  interface IUserService
    {
        IList<AppUser> GetAllUsers();
        AppUser GetUserById(int id);
        AppUser GetCurrentUser(string name);
        decimal GetUserAmount(string name);
        void RegisterUser(AppUser user);
        void ChangePassword(string email, string password);


    }
}
