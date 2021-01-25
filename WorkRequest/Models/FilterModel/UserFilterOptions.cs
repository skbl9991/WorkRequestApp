using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models.FilterModel
{
    public class UserFilterOptions
    {
        public string FIO { get; set; }
        public string Logon { get; set; }
        public string Phone { get; set; }
        public string Roles { get; set; }
    }
}
