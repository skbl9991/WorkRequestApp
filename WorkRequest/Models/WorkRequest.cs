using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFJunctions;

namespace WorkRequestManagment.Models
{
    #region ColorsFor Statuses
    //Created: text-info #17a2b8
    //InProgress: text-primary #007bff
    //Done: text-success #28a745
    //Canceled: text-secondary #6c757d
    //Deleted/Removed: text-danger #dc3545
    #endregion
    public enum Statuses{
        Created, InProgress, Done, Canceled, All
    }

    public class WorkRequest
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(700, MinimumLength = 5, ErrorMessage = "Описание проблемы должно содержать от 5 до 700 символов")]
        public string RequestMessage { get; set; }

        [Column(TypeName = "tinyint")]
        public Statuses CurentStatus { get; set; }

        public IEnumerable<WorkRequestUserJunction> WorkRequestUser { get; set; }

        //public IEnumerable<string> UpdateLog { get; set; }
    }

}
