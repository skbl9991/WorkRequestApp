using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkRequestManagment.Models;
using WorkRequestManagment.Models.EFContexts;
using WorkRequestManagment.Models.FilterModel;
using WorkRequestManagment.Models.Pages;
using WorkRequestManagment.Models.ViewModels;

namespace WorkRequestManagment.Controllers
{
    public class AdminUserController : Controller
    {
        public EFWorkRequestContext context;

        //check role Admin User
        string testLogon = "MINSK\\Arc_RA";

        public AdminUserController(EFWorkRequestContext  ctx)
        {
            context = ctx;
        }


        public IActionResult SomeIndex()
        {
            // The expression tree to execute.  
            BinaryExpression be = Expression.Power(Expression.Constant(2D), Expression.Constant(3D));
            // Create a lambda expression.  
            Expression<Func<double>> le = Expression.Lambda<Func<double>>(be);

            Func<double> compiledExpression = le.Compile();
            double result = compiledExpression();

            return View();
        }

        //public IActionResult Index(QueryOptions options, FilterOptions filterOptions)
        public IActionResult Index(QueryOptions options, UserFilterOptions filterOptions)
        {
            var adminUser = context.Users.Include(u => u.WorkRequestUser).FirstOrDefault(u => u.LogonName == testLogon);
            var usersList = context.Users.Include(u => u.WorkRequestUser).Where(u => u.Id != adminUser.Id);

            //Filter options
            //if (filterOptions?.Role != null){
            //    usersList = usersList.Where(u => u.Role == filterOptions.Role);
            //}

            //if (!string.IsNullOrEmpty(filterOptions?.PropName) && !string.IsNullOrEmpty(filterOptions?.PropValue))
            //{

            //}

            return View(new UserListViewModel
            {
                AdminUser = adminUser,
                FilterOptions = filterOptions,
                Users = new PagedList<User>(usersList, options)
            });;
        }

        public IActionResult ResetFilter()
        {
            return RedirectToAction("Index");
        }
    }
}
