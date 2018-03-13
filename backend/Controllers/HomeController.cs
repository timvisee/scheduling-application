using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Types;
using Type = System.Type;

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
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public void SeedDb()
        {
            //delete all existing data
            var users =_context.Users.ToList();
            var events = _context.Events.ToList();
            _context.Users.RemoveRange(users);
            _context.Events.RemoveRange(events);
            //generate users
            for (var i = 0; i < 10; i++)
            {
                var user = new User
                {
                    FirstName = "user" + i,
                    Infix = " ",
                    LastName = "lastName" + i,
                    Locale = "nl_NL",
                    //TODO PEOPLE parameter here
                    Type = Types.Type.Student,
                    Role = Role.Basic
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            //generate events
            for (var i = 0; i < 8; i++)
            {
                var ev = new Event
                {
                    Start = new DateTime(2018, 1, 1, 8 + i, 0, 0, 0),
                    End = new DateTime(2018, 1, 1, 9 + i, 0, 0, 0),
                    Description = "Description of the event",
                    Title = "Title of Event",
                    //TODO List of people
                };
            }
        }
    }
}
