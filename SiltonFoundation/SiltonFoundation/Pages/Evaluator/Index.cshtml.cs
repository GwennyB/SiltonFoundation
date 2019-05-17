using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiltonFoundation.Pages.Evaluator
{
    [Authorize(Roles = ("Evaluator"))]
    public class IndexModel : PageModel
    {
        // TODO: build routes for profile mgmt, application viewing, scoring
        public void OnGet()
        {
        }
    }
}