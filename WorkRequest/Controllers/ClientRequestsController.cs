using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private string testUserLogon = "MINSK\\Arc_CL"; //, Role = Roles.Client
        private EFWorkRequestContext context;

        public ClientRequestsController(EFWorkRequestContext ctx) => context = ctx;
        
        public IActionResult List()
        {
            var user = context.Users.Where(u => u.LogonName == testUserLogon)
                .FirstOrDefault();

            if (user == null)
                return NotFound("User not found"); //redirect to CreateUser or Error Page

            var workRequests = context.WorkRequestUserJunctions
                    .Include(wru => wru.User)
                    .Include(wru => wru.WorkRequest)
                    .Where(wrj => wrj.UserId == user.Id)
                    .Where(wr => wr.WorkRequest.CurentStatus == Statuses.Created
                                                        || wr.WorkRequest.CurentStatus == Statuses.InProgress);

            var model = new UserWorkRequestViewModel
            {
                User = user,
                WorkRequests = workRequests.Select(wrj => wrj.WorkRequest).ToList()
            };
            return View(model);
        }


        public IActionResult AddOrUpdate(int? editWorkRequestId)
        {
            if (editWorkRequestId == null) {
                return View(new WorkRequest { Id = 0, CurentStatus = Statuses.Created, RequestMessage = null });
            } 

            var requestToEdit = context.WorkRequests
                .Include(wr => wr.WorkRequestUser)
                .FirstOrDefault(wr => wr.Id == editWorkRequestId);

            if (requestToEdit == null)
                return NotFound();

            return View(requestToEdit);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(WorkRequest updateWorkRequest)
        {
            if (updateWorkRequest == null) 
                return NoContent();

            if (updateWorkRequest.Id == 0) {
                //create Workrequest
                var user = context.Users.FirstOrDefault(u => u.LogonName == testUserLogon);

                context.WorkRequestUserJunctions.Add(new WorkRequestUserJunction
                {
                    UserId = user.Id,
                    WorkRequest = updateWorkRequest
                });
                context.SaveChanges();
                return RedirectToAction(nameof(List));
            }

            context.WorkRequests.Update(updateWorkRequest);
            context.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public IActionResult DeleteWorkRequest(long? deletedWorkRequestId)
        {
         
            if (deletedWorkRequestId == null) return NotFound();
            
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
