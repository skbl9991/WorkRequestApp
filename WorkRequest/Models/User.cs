using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFJunctions;

namespace WorkRequestManagment.Models
{
    public enum Roles
    {
        Client, Executor, RoleAdmin, Inspector, MainAdmin
    }

    public class User
    {
        public long Id { get; set; }
        public string LogonName { get; set; } //Ad logon
        public string UserFIO { get; set; }
        public string Phone { get; set; }
        public Roles Role { get; set; }
        //public IEnumerable<string> Groups { get; set; } // group

        public IEnumerable<WorkRequestUserJunction> WorkRequestUser { get; set; }
    }
}
