using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models.EFJunctions
{
    public class WorkRequestUserJunction
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long WorkRequestId { get; set; }
        public WorkRequest WorkRequest { get; set; }
    }
}
