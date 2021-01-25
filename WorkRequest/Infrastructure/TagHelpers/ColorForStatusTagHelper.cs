using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WorkRequestManagment.Models;

namespace WorkRequestManagment.Infrastructure.TagHelpers
{

    [HtmlTargetElement(Attributes = "status-code, fill-or-color")]
    public class ColorForStatusTagHelper : TagHelper
    {
        //get color class for Enter Status
        public Statuses StatusCode { get; set; } = Statuses.Created;
        public string FillOrColor { get; set; } = "color";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string className = StatusHelper.GetPartOfClassByStatusName(StatusCode);
            //get 
            if (FillOrColor.Equals("color", StringComparison.OrdinalIgnoreCase)){
                className = $"text-{className}";
            }
            else if (FillOrColor.Equals("fill", StringComparison.OrdinalIgnoreCase))
            {
                className = $"fill-{className}";
            } else {
                className = "";
            }
            //Add this class to attribute
            output.AddClass(className, HtmlEncoder.Default);

        }
    }
}
