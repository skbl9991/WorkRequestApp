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
    public class AdminCardViewComponent : ViewComponent
    {
        private EFWorkRequestContext wrContext;

        public AdminCardViewComponent(EFWorkRequestContext context) => wrContext = context;

        public async Task<IViewComponentResult> InvokeAsync(User userModel)
        {
            if (userModel == null)
                return Content($"User modal was empty !");

            var viewModel = new AdminCardViewModel
            {
                User = userModel,
                ClientCount = await wrContext.Users.CountAsync(ur => ur.Role == Roles.Client),
                ExecutorCount = await wrContext.Users.CountAsync(ur => ur.Role == Roles.Executor),
                InspectorCount = await wrContext.Users.CountAsync(ur => ur.Role == Roles.Inspector),
                MainAdminCount = await wrContext.Users.CountAsync(ur => ur.Role == Roles.MainAdmin),
                RoleAdminCount = await wrContext.Users.CountAsync(ur => ur.Role == Roles.RoleAdmin),
            };
            return View(viewModel);
        }
    }
}
