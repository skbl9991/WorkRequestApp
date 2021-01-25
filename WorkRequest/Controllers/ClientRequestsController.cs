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

    //Updates
    //SOLID for acces to data
    //more beutiful design for Views
    //add DTO for create and updates data
    //add async for work with context
    public class ClientRequestsController : Controller
    {
        private string testUserLogon = "MINSK\\Arc_CL"; //, Role = Roles.Client
        private EFWorkRequestContext context;

        public ClientRequestsController(EFWorkRequestContext ctx) => context = ctx;

        //status == nul equal to Statuses.All
        public IActionResult Index(QueryOptions options, Statuses? status = null)
        {
            var user = context.Users.Where(u => u.LogonName == testUserLogon)
                .FirstOrDefault(); //1 запрос для всего контроллера
            
            if (user == null)
                return NotFound("User not found"); //redirect to CreateUser or Error Page

            var data = context.WorkRequestUserJunctions
                 .Include(wru => wru.User)
                 .Include(wru => wru.WorkRequest)
                 .Where(wrj => wrj.UserId == user.Id)
                 .Select(wrj => wrj.WorkRequest);

            //Filter options
            if (status != null){
                data = data.Where(wr => wr.CurentStatus == status);
            }
    
            var model = new UserWorkRequestViewModel
            {
                User = user,
                WorkRequests = new PagedList<WorkRequest>(data, options)
            };
            return View(model);
        }


        public IActionResult AddOrUpdate(int? editWorkRequestId)
        {
            if (editWorkRequestId == null){
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
        [ValidateAntiForgeryToken]
        public IActionResult AddOrUpdate(WorkRequest updateWorkRequest)
        {
            if (updateWorkRequest == null) 
                return NotFound();

            if (ModelState.IsValid)
            {
                if (updateWorkRequest.Id == 0){
                    //add New Workrequest
                    var user = context.Users.FirstOrDefault(u => u.LogonName == testUserLogon);
                    context.WorkRequestUserJunctions.Add(new WorkRequestUserJunction  {
                        UserId = user.Id,
                        WorkRequest = updateWorkRequest
                    });
                }
                else { 
                    //Update
                    context.WorkRequests.Update(updateWorkRequest);
                }

                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(updateWorkRequest);
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
