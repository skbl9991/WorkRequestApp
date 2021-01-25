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

        public IActionResult Index(QueryOptions options, Statuses? status = null)
        {
            ViewBag.SelectedStatus = status; //save selected status
            ViewBag.ReturnUrl = HttpContext.Request.Path + HttpContext.Request.QueryString; // Request.Headers["Referer"].ToString();

            var userExec = context.Users.FirstOrDefault(u => u.LogonName == testUserLogon);

            if (userExec == null)
                return NotFound(); //redirect to CreateUser or Error Page

            IQueryable<WorkRequest> data = context.WorkRequests.Include(wr => wr.WorkRequestUser);

            if (status == Statuses.Created) // Not Accepted WorkRequests
            {
                data = data.Where(wr => wr.CurentStatus == status); //Executor must see all Created Requests
            }
            else if (status != null)
            {
                data = context.WorkRequestUserJunctions
                     .Include(wrj => wrj.User)
                     .Include(wrj => wrj.WorkRequest)
                     .Where(wrj => wrj.UserId == userExec.Id)
                     .Where(wrj => wrj.WorkRequest.CurentStatus == status)
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
        [ValidateAntiForgeryToken]
        public IActionResult AcceptRequest(long? acceptedRequestId, string returnUrl)
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

            return Redirect(returnUrl);
        }

        [HttpPost] //Rework This method
        [ValidateAntiForgeryToken]
        public IActionResult DoneWorkRequest(long? doneWorkRequestId, string returnUrl)
        {

            if (doneWorkRequestId == null) return NotFound();

            var requestForUpdate = context.WorkRequests
                .Include(wr => wr.WorkRequestUser)
                .FirstOrDefault(wr => wr.Id == doneWorkRequestId);

            if (requestForUpdate == null)
                return NotFound();

            requestForUpdate.CurentStatus = Statuses.Done;
            context.SaveChanges();

            return Redirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWorkRequest(long? deletedWorkRequestId, string returnUrl)
        {
            if (deletedWorkRequestId == null) return NotFound();

            var requestForDelete = context.WorkRequests
                .Include(wr => wr.WorkRequestUser)
                .FirstOrDefault(wr => wr.Id == deletedWorkRequestId);

            if (requestForDelete == null)
                return NotFound();

            context.WorkRequests.Remove(requestForDelete);
            context.SaveChanges();

            return Redirect(returnUrl);
        }

    }
}
