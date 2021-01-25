using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.FilterModel;
using WorkRequestManagment.Models.Pages;

namespace WorkRequestManagment.Models.ViewModels
{
    public class UserListViewModel
    {
        public User AdminUser { get; set; }
        public PagedList<User> Users { get; set; }
        public UserFilterOptions FilterOptions { get; set; }
    }
}
