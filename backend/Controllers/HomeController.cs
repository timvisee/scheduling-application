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

        public IActionResult Index()
        {
            DateTime startDate = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
            DateTime endDate = startDate.AddDays(6).AddHours(23).AddMinutes(59);

            var events = _context.Events.Where(x => x.Start >= startDate && x.End <= endDate);

            List <List<Event>> week = new List<List<Event>>();

            for (int i = 0; i < 5; i++)
            {
                var day = events.Where(x => x.Start >= startDate.AddDays(i) && x.End < startDate.AddDays(i + 1)).ToList();
                week.Add(day);
            }

            ViewData["week"] = week;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
