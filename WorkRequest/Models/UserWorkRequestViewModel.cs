using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models
{

    public class UserWorkRequestViewModel
    {
        public User User { get; set; }
        public IEnumerable<WorkRequest> WorkRequests { get; set; }


    }
}
