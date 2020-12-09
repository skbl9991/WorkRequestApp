using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFJunctions;

namespace WorkRequestManagment.Models
{
    public enum Statues
    {
        Created, InProgress, Done, Canceled
    }

    public class WorkRequest
    {
        public long Id { get; set; }
        public string RequestMessage { get; set; }
        public Statues CurentStatus { get; set; }

        public IEnumerable<WorkRequestUserJunction> WorkRequestUser { get; set; }

        //public IEnumerable<string> UpdateLog { get; set; }
    }

}
