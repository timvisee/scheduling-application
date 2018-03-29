using backend.Data;
using backend.Extensions;
using backend.Models;
using backend.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Landing page for users after login
         */
        public IActionResult Index()
        {
            // Get the start and end dates for the current date range
            DateTime start = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
            DateTime end = start.AddDays(6).AddHours(23).AddMinutes(59);

            // Fetch all relevant events
            var events = _context.Events.Where(x => x.Start >= start && x.End <= end);

            if (!events.Any())
            {
                return View();
            }
            // Build a list of days, containing a list of events on that day
            ViewBag.Days = Enumerable.Range(0, 6)
                .Select(
                    day => events.Where(
                        x => x.Start >= start.AddDays(day)
                            && x.End < start.AddDays(day + (day == 5 ? 2 : 1))
                    ).ToList()
                ).ToList();

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
