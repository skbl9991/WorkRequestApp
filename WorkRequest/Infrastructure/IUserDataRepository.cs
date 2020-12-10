using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models;

namespace WorkRequestManagment.Infrastructure
{
    //Declare access to Users functions
    //Replace by User Identity
    public interface IUserDataRepository
    {
        User GetUser(long id);
        User GetUSer(string logonName);
        IEnumerable<User> GetAllUsers(bool includeWorkRequests = false);
        IEnumerable<User> GetFilteredUsers(string logonName = null, string userFIO = null,
            string phone = null, int[] roles = null);
        void CreateUser(User newUser);
        void UpdateUser(User changedUser, User originalUser = null);
        void DeleteUser(long id);
    }
}
