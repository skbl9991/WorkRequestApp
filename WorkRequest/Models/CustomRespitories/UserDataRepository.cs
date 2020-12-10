using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Infrastructure;
using WorkRequestManagment.Models.EFContexts;

namespace WorkRequestManagment.Models.CustomRespitories
{
    public class UserDataRepository : IUserDataRepository
    {
        private EFWorkRequestContext context;
        public UserDataRepository(EFWorkRequestContext ctx) => context = ctx;

        public void CreateUser(User newUser)
        {
            newUser.Id = 0;
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public void DeleteUser(long id)
        {
            User userToDelete = GetUser(id);
            context.Users.Remove(userToDelete);
            //remove snapped objectto User if this need
            context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers(bool includeWorkRequests = false)
        {
            IQueryable<User> data = context.Users;
            if (includeWorkRequests)
            {
                data = data.Include(u => u.WorkRequestUser).ThenInclude(wr => wr.WorkRequest);
            }
            return data.Include(u => u.WorkRequestUser);
        }

        public IEnumerable<User> GetFilteredUsers(string logonName = null, string userFIO = null, string phone = null, int[] roles = null)
        {
            IQueryable<User> data = context.Users;
            if (!String.IsNullOrEmpty(logonName))
            {
                data = data.Where(u => u.LogonName.Equals(logonName, StringComparison.OrdinalIgnoreCase));
            }
            if (!String.IsNullOrEmpty(userFIO))
            {
                data = data.Where(u => u.UserFIO.Contains(userFIO, StringComparison.OrdinalIgnoreCase));
            }
            if (!String.IsNullOrEmpty(phone))
            {
                data = data.Where(u => u.Phone.Contains(phone, StringComparison.OrdinalIgnoreCase));
            }
            if (roles != null && roles?.Length > 0)
            {
                data = data.Where(u => roles.Contains((int)u.Role));
            }

            return data;
        }

        public User GetUser(long id) => context.Users.Find(id); //что быстрее Find иди FirstOrDefault

        public User GetUSer(string logonName) => context.Users.Find(logonName);

        public void UpdateUser(User changedUser, User originalUser = null)
        {
            if (originalUser == null)
            {
                originalUser = context.Users.Find(changedUser.Id);
            }
            else
            {
                context.Users.Attach(originalUser);
            }
            originalUser.LogonName = changedUser.LogonName;
            originalUser.Phone = changedUser.Phone;
            originalUser.Role = changedUser.Role;
            originalUser.UserFIO = changedUser.UserFIO;
            originalUser.WorkRequestUser = changedUser.WorkRequestUser;
            context.SaveChanges();
        }
    }
}
