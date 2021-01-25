using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models.Pages
{
    public class QueryOptions
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string OrderPropertyName { get; set; }
        public bool DescendingOrder { get; set; }

    }


}
