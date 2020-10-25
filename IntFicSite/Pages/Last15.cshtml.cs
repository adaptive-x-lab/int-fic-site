using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntFicSite.Pages
{
    public class Last15Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Node { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Session { get; set; }

        public IActionResult OnGet()
        {
            return new JsonResult(new {satus = "OK",site="Last15", node=Node,session=Session});
        }
    }
}
