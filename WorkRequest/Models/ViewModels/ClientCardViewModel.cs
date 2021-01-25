using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models.ViewModels
{
    public class ClientCardViewModel
    {
        public User User { get; set; }
        public int CreatedRequestCount { get; set; }
        public int InProgressRequesCount { get; set; }

    }
}
