using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Controllers
{
    public class ToolsController : Controller
    {
        public IActionResult BdList()
        {
            return View();
        }
    }
}
