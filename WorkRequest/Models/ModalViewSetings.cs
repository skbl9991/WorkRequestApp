using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models
{
    public class ModalViewSetings
    {
        public string ModalId { get; set; }
        public string Title { get; set; }
        public string BodyText { get; set; }
        public string ModelStyle { get; set; } = "primary";
    }
}
