using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models;
using WorkRequestManagment.Models.EFContexts;
using WorkRequestManagment.Models.EFJunctions;
using WorkRequestManagment.Models.Pages;
using WorkRequestManagment.Models.ViewModels;

namespace WorkRequestManagment.Controllers
{
    public class ExecutorRequestsController : Controller
    {

        private string testUserLogon = "MINSK\\Arc_EX"; //, Role = Roles.Client
        private EFWorkRequestContext context;

        public ExecutorRequestsController(EFWorkRequestContext ctx) {
            context = ctx;
        }

        public IActionResult Index(QueryOptions options, int status = (int)Statuses.InProgress)
        {
            var userExec = context.Users.FirstOrDefault(u => u.LogonName == testUserLogon);

            if (userExec == null)
                return NotFound(); //redirect to CreateUser or Error Page

            IQueryable<WorkRequest> data = context.WorkRequests.Include(wr => wr.WorkRequestUser);

            if (status == (int)Statuses.Created) // Not Accepted WorkRequests
            {
                data = data.Where(wr => wr.CurentStatus == (Statuses)status);
            }
            else if (status != (int)Statuses.All)
            {
                data = context.WorkRequestUserJunctions
                     .Include(wrj => wrj.User)
                     .Include(wrj => wrj.WorkRequest)
                     .Where(wrj => wrj.UserId == userExec.Id)
                     .Where(wrj => wrj.WorkRequest.CurentStatus == (Statuses)status)
                     .Select(wrj => wrj.WorkRequest);
            }

            var model = new UserWorkRequestViewModel
            {
                User = userExec,
                WorkRequests = new PagedList<WorkRequest>(data, options)
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult AcceptRequest(long? acceptedRequestId)
        {
            if (acceptedRequestId == null) 
                return NotFound();

            var user = context.Users.FirstOrDefault(u => u.LogonName == testUserLogon);
            var acceptedWorkRequest = context.WorkRequests
                .Include(wr => wr.WorkRequestUser)
                .FirstOrDefault(wr => wr.Id == acceptedRequestId);

            if (acceptedWorkRequest == null)
                return NotFound();

            acceptedWorkRequest.CurentStatus = Statuses.InProgress;

            context.WorkRequestUserJunctions.Add(new WorkRequestUserJunction
            {
                UserId = user.Id,
                WorkRequest = acceptedWorkRequest
            });

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost] //Rework This method
        public IActionResult DoneWorkRequest(long? doneWorkRequestId)
        {

            if (doneWorkRequestId == null) return NotFound();

            var requestForUpdate = context.WorkRequests
                .Include(wr => wr.WorkRequestUser)
                .FirstOrDefault(wr => wr.Id == doneWorkRequestId);

            if (requestForUpdate == null)
                return NotFound();

            requestForUpdate.CurentStatus = Statuses.Done;
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
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

            return RedirectToAction(nameof(Index));
        }

    }
}
