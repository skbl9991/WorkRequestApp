using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.Pages;

namespace WorkRequestManagment.Models.ViewModels
{

    public class UserWorkRequestViewModel
    {
        public User User { get; set; }
        public PagedList<WorkRequest> WorkRequests { get; set; }


    }
}
