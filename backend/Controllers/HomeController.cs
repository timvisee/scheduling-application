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
using System.Net;
using System.IO;

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
            return View(_context.Events.ToList());
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

        public void Seed()
        {
//          delete all existing data
            Console.WriteLine("Deleting all data...");
            _context.Users.Clear();
            _context.Peoples.Clear();
            _context.Groups.Clear();
            _context.Locations.Clear();
            _context.Events.Clear();
            _context.EventLocations.Clear();
            _context.UserGroups.Clear();
            _context.SaveChanges();

            Console.WriteLine("Generating users and people..");
            for (var i = 0; i < 10; i++)
            {
                var ppl = new People();
                _context.Peoples.Add(ppl);
                _context.SaveChanges();

                var user = new User
                {
                    FirstName = "user" + i,
                    Infix = " ",
                    LastName = "lastName" + i,
                    Locale = "nl_NL",
                    PeopleId = ppl.PeopleId,
                    Type = Types.Type.Student,
                    Role = Role.Basic
                };
                _context.Users.Add(user);
            }
            _context.SaveChanges();

            Console.WriteLine("Generating groups..");

            var people = new People();
            _context.Peoples.Add(people);
            _context.SaveChanges();

            var group = new Group
            {
                Name = "Informatics class 1",
                PeopleId = people.PeopleId
            };
            _context.Groups.Add(group);
            _context.SaveChanges();

            Console.WriteLine("Pairing users and groups...");
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                var ug = new UserGroup
                {
                    User = user,
                    Group = group
                };
                _context.UserGroups.Add(ug);
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

        public void loadEventfromIcal()
        {
            // haal rooster op via url (liefst met param group + date (yyyy-month-day)

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://ruz.spbstu.ru/faculty/100/groups/25242/ical?date=2018-3-12");
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            // lees alle events in 
            string ical = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            Console.WriteLine(ical);

            char[] delim = { '\n' };
            string[] lines = ical.Split(delim);
            delim[0] = ':';
            //split lines en maak array met data per events. 
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("BEGIN:VEVENT"))
                {
                    string[] eventData = new string[9];
                    for (int j = 0; j < 9; j++)
                        eventData[j] = lines[i + j + 1].Split(delim)[1];
                    ///////////////////////////
                    //Do your SQL INSERT here //
                    ///////////////////////////

                    var ev = new Event
                    {
                        Start = Convert.ToDateTime(eventData[4]),
                        End = Convert.ToDateTime(eventData[5]),
                        Title = eventData[6]
                    };
                    _context.Events.Add(ev);
                    _context.SaveChanges();


                    i += 10;
                }
            }
        }
    }

}
