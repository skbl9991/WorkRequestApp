using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Infrastructure;
using WorkRequestManagment.Models;
using WorkRequestManagment.Models.EFContexts;
using WorkRequestManagment.Models.EFJunctions;

namespace WorkRequestManagment.Controllers
{
    public class ClientRequestsController : Controller
    {
        private string testUserLogon  = "MINSK\\Arc_CL"; //, Role = Roles.Client
        private EFWorkRequestContext context;

        public ClientRequestsController(EFWorkRequestContext ctx) => context = ctx;

        public IActionResult List(){

            Console.WriteLine("\n \n List -------------------------------");
            var workRequests = context.Set<WorkRequestUserJunction>()
                .Include(wr => wr.User)
                .Include(wr => wr.WorkRequest)
                .Where(wr => wr.User.LogonName == testUserLogon).AsNoTracking().ToArray();

            var model = new UserWorkRequestViewModel
            {
                User = workRequests.Select(wr => wr.User).First(),
                WorkRequests = workRequests.Select(wr => wr.WorkRequest).ToArray()
            };
            return View(model);
        }

        public IActionResult CreateWorkRequest()
        {
            var wRequest = new WorkRequest
            {
                Id = 0,
                CurentStatus = Statuses.Created,
                RequestMessage = ""
            };
            return View(wRequest);
        }

        [HttpPost]
        public IActionResult CreateWorkRequest(WorkRequest newWorkRequest)
        {
            Console.WriteLine("\n \n CreateWorkRequest -------------------------------");
            var user = context.Users
                //.Include(u => u.WorkRequestUser)
                //.ThenInclude(wr => wr.WorkRequest)
                .FirstOrDefault(u => u.LogonName == testUserLogon)?.Id;

            context.Set<WorkRequestUserJunction>()
                .Add(new WorkRequestUserJunction
                {
                    UserId = user.Value,
                    WorkRequest = newWorkRequest
                });
            context.SaveChanges();
            return RedirectToAction(nameof(List));
        }

    }
}
