using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models.ViewModels
{
    public class AdminCardViewModel
    {
        public User User { get; set; }
        public int ClientCount { get; set; }
        public int ExecutorCount { get; set; }
        public int InspectorCount { get; set; }
        public int MainAdminCount { get; set; }
        public int RoleAdminCount { get; set; }

     
        public int GetCountByRoleName(string roleName)
        {
            var countList = this.GetType().GetProperties();
            var prop = countList
                .FirstOrDefault(str => str.Name.Contains(roleName, StringComparison.OrdinalIgnoreCase));
            if (prop == null)
                return 0;

            //return (int)this.GetType().GetProperty(prop.Name).GetValue(this, null);
            return (int)GetPropValue(this, prop.Name);
        }

        private object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

    }

    

}
