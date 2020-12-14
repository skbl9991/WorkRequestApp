using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFContexts;
using WorkRequestManagment.Models.EFJunctions;

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
                        requestContext.WorkRequests.AddRange(Requests);

                    if (requestContext.Users.Count() == 0)
                        requestContext.Users.AddRange(Users);

                    if (requestContext.WorkRequestUserJunctions.Count() == 0)
                        requestContext.WorkRequestUserJunctions.AddRange(WorkRequestUserJunctions);

                    context.SaveChanges();
                }
            }
        }

        public static void ClearData(DbContext context)
        {
            if (context is EFWorkRequestContext requestContext)
            {
                if (requestContext.WorkRequestUserJunctions.Count() != 0) 
                    requestContext.WorkRequestUserJunctions
                        .RemoveRange(requestContext.WorkRequestUserJunctions);
                

                if (requestContext.WorkRequests.Count() != 0) 
                    requestContext.WorkRequests.RemoveRange(requestContext.WorkRequests);
                
                if (requestContext.Users.Count() != 0) 
                    requestContext.Users.RemoveRange(requestContext.Users);

                context.SaveChanges();
            }
  
        }

        //Add to user client Workrequests with status = created
        public static WorkRequestUserJunction[] WorkRequestUserJunctions {
            get {
                WorkRequestUserJunction[] junctons = 
                {
                    new WorkRequestUserJunction { UserId = 1, WorkRequestId = 1 },
                     new WorkRequestUserJunction { UserId = 1, WorkRequestId = 9 },
                      new WorkRequestUserJunction { UserId = 1, WorkRequestId = 10 },
                       new WorkRequestUserJunction { UserId = 1, WorkRequestId = 12 },
                        new WorkRequestUserJunction { UserId = 1, WorkRequestId = 13 },
                         new WorkRequestUserJunction { UserId = 1, WorkRequestId = 15 },
                };
                return junctons;
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
