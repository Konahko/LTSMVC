using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;
using LTSMVC.Classes.Journals;

namespace LTSMVC.Controllers.Jobs
{
    public class TasksList : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
