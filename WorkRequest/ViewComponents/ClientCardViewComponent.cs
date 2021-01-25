using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models;
using WorkRequestManagment.Models.EFContexts;
using WorkRequestManagment.Models.ViewModels;

namespace WorkRequestManagment.ViewComponents
{
    public class ClientCardViewComponent : ViewComponent
    {
        private EFWorkRequestContext wrContext;
        public ClientCardViewComponent(EFWorkRequestContext ctx) => wrContext = ctx;

        public async Task<IViewComponentResult> InvokeAsync(User userModel)
        {
            if (userModel == null)
                return Content($"User modal was empty !");

           var viewModel = new ClientCardViewModel
            {
                User = userModel,
                CreatedRequestCount = await wrContext.WorkRequests
                    .CountAsync(wr => wr.CurentStatus == Models.Statuses.Created),

                InProgressRequesCount = await wrContext.WorkRequestUserJunctions
                    .Include(u => u.User)
                    .Include(wr => wr.WorkRequest)
                    .CountAsync(wrj => wrj.WorkRequest.CurentStatus == Models.Statuses.InProgress && wrj.UserId == userModel.Id)
            };
            return View(viewModel);
        }


    }
}
