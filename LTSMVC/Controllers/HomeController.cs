﻿using LTSMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Lts2Context db;

        public HomeController(ILogger<HomeController> logger, Lts2Context context)
        {
            _logger = logger;
            db = context;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public async Task<IActionResult> User_listAsync()
        {
            var items = await db.Staff.Include(x => x.AddressBooks).ToListAsync();
            var item = items.FirstOrDefault();
            item.AddressBooks.Where(x => x.StaffId == 1);
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
