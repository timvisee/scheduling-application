using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Extensions;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Types;
using webapp.Models;
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
//          delete all existing data
            Console.WriteLine("Deleting all data...");
            _context.Users.Clear();
            _context.Locations.Clear();
            _context.Events.Clear();
            _context.EventLocations.Clear();
            _context.SaveChanges();

            //seed
            Console.WriteLine("Generating users..");
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
            }

            Console.WriteLine("Generating locations..");
            var locations = 4;
            for (var i = 0; i < locations; i++)
            {
                var location = new Location
                {

                    Description = "description " + i,
                    Latitude = 10 + i * 5,
                    Longitude = 100 - (i * 5),
                    Name = "location " + i,
                };
                _context.Locations.Add(location);
            }


            Console.WriteLine("Generating events..");
            for (var i = 0; i < 4; i++)
            {
                var ev = new Event
                {
                    Start = new DateTime(2018, 1, 1, 8 + i, 0, 0, 0),
                    End = new DateTime(2018, 1, 1, 9 + i, 0, 0, 0),
                    Description = "Description of the event",
                    Title = "Title of Event",
                    //TODO List of people
                };
                _context.Events.Add(ev);
            }

            _context.SaveChanges();

            Console.WriteLine("Pairing locations and events...");

            var locs = _context.Locations.ToList();
            var count = 0;
            foreach (var ev in _context.Events)
            {
                Random rnd = new Random();
                var el = new EventLocation
                {
                    Event = ev,
                    Location = locs[count]
                };
                _context.EventLocations.Add(el);
                count++;
            }
            _context.SaveChanges();
            Console.WriteLine("Seeded database.");
        }
    }
}
