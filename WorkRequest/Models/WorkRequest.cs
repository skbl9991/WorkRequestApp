﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFJunctions;

namespace WorkRequestManagment.Models
{
    public enum Statuses{
        Created, InProgress, Done, Canceled, All
    }

    public class WorkRequest
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        [MinLength(5, ErrorMessage = "Описание проблемы должно содержать не менее 5 символов")]
        [MaxLength(700, ErrorMessage = "Описание проблемы должно содержать не более 700 символов")]
        public string RequestMessage { get; set; }
        [Column(TypeName = "tinyint")]
        public Statuses CurentStatus { get; set; }

        public IEnumerable<WorkRequestUserJunction> WorkRequestUser { get; set; }

        //public IEnumerable<string> UpdateLog { get; set; }
    }

}
