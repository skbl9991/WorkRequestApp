using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFJunctions;

namespace WorkRequestManagment.Models
{

    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(30, ErrorMessage = "Описание проблемы должно содержать не более 30 символов")]
        public string LogonName { get; set; } //Ad logon

        [StringLength(50, ErrorMessage = "Описание проблемы должно содержать не более 50 символов")]
        public string UserFIO { get; set; }

        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(30, ErrorMessage = "Описание проблемы должно содержать не более 30 символов")]
        public string Phone { get; set; }

        [Column(TypeName = "tinyint")]
        public Roles Role { get; set; }
        //public IEnumerable<string> Groups { get; set; } // group

        public IEnumerable<WorkRequestUserJunction> WorkRequestUser { get; set; }
    }
}
