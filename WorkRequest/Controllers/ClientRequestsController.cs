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

    //Updates
    //one view for create and update
    //SOLID for acces to data
    //more beutiful design for Views
    //add validation for views
    //add DTO for create and updates data
    //add async 
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
            Console.WriteLine("\n \n CreateWorkRequest ------------------------------- \n");
            var user = context.Users
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

        public IActionResult UpdateWorkRequest(int? editWorkRequestId)
        {
            Console.WriteLine("\n \n UpdateWorkRequest ------------------------------- \n");
            //get WorkRequest 
            if (editWorkRequestId == null)
                return NotFound();

            //нужны ли здесь Include запросы ?
            var requestToEdit = context.WorkRequests
                .Include(wr => wr.WorkRequestUser)
                .ThenInclude(wru => wru.User).FirstOrDefault(wr => wr.Id == editWorkRequestId);

            if (requestToEdit == null)
                return NotFound();

            return View(requestToEdit);
        }

        [HttpPost]
        public IActionResult UpdateWorkRequest(WorkRequest updateWorkRequest)
        {
            if (updateWorkRequest == null){
                return NoContent();
            }
            context.WorkRequests.Update(updateWorkRequest);
            context.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public IActionResult DeleteWorkRequest(long? deletedWorkRequestId)
        {
            Console.WriteLine("\n \n ---------- Delete Method------- \n");

            if (deletedWorkRequestId == null){
                return NotFound();
            }

            var requestForDelete = context.WorkRequests
                .Include(wr => wr.WorkRequestUser)
                .FirstOrDefault(wr => wr.Id == deletedWorkRequestId);

            if (requestForDelete == null)
                return NotFound();

            context.WorkRequests.Remove(requestForDelete);
            context.SaveChanges();

            return RedirectToAction(nameof(List));
        }
    }
}
