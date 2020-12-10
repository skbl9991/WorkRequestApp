using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFContexts;

namespace WorkRequestManagment.Models
{
    public static class SeedData
    {
        public static void Seed(DbContext context)
        {
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context is EFWorkRequestContext requestContext)
                {
                    //add data To Database
                    if (requestContext.WorkRequests.Count() == 0)
                    {
                        requestContext.WorkRequests.AddRange(Requests);
                    }
                    if (requestContext.Users.Count() == 0)
                    {
                        requestContext.Users.AddRange(Users);
                    }

                }

                context.SaveChanges();
            }
        }

        public static void ClearData(DbContext context)
        {
            if (context is EFWorkRequestContext requestContext)
            {
                if (requestContext.WorkRequests.Count() == 0)
                {
                    requestContext.WorkRequests.RemoveRange(requestContext.WorkRequests);
                }
                if (requestContext.Users.Count() == 0)
                {
                    requestContext.Users.RemoveRange(requestContext.Users);
                }
            }
        }

        public static WorkRequest[] Requests
        {
            get
            {
                string generated = System.IO.File.ReadAllText("generated_WorkRequests.json");
                var workRequests = JsonSerializer.Deserialize<List<WorkRequest>>(generated);
                return workRequests.ToArray();
            }
        }

        public static User[] Users
        {
            get
            {
                string generated = System.IO.File.ReadAllText("generated-users.json");
                var users = JsonSerializer.Deserialize<List<User>>(generated);
                users.AddRange(
                    new User[]{
                    new User { LogonName = "MINSK\\Arc_MA", Phone = "111-111", UserFIO = "Arc MainAdmin", Role = Roles.MainAdmin },
                    new User { LogonName = "MINSK\\Arc_EX", Phone = "222-222", UserFIO = "Arc Executor", Role = Roles.Executor },
                    new User { LogonName = "MINSK\\Arc_IN", Phone = "313-123", UserFIO = "Arc Inspector", Role = Roles.Inspector },
                    new User { LogonName = "MINSK\\Arc_RA", Phone = "513-373", UserFIO = "Arc RoleAdmin", Role = Roles.RoleAdmin },
                    new User { LogonName = "MINSK\\Arc_CL", Phone = "777-373", UserFIO = "Arc ClientRole", Role = Roles.Client },
                    }
                );

                return users.ToArray();
            }
        }
    }
}
