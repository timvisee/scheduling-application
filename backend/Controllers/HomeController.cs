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
using System.IO;
using System.Net;

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
                    DateStart = new DateTime(2018, 1, 1, 8 + i, 0, 0, 0),
                    DateEnd = new DateTime(2018, 1, 1, 9 + i, 0, 0, 0),
                    Description = "Description of the event",
                    Title = "Title of Event",
                    //TODO List of people
                };
                _context.Events.Add(ev);
                _context.SaveChanges();
            }
        }
        public void loadEventfromIcal(//to do make param GROUP + date and make some sort of generator to collect time tiables. )
        {
            // haal rooster op via url (liefst met param group + date

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
                    { DateStart = Convert.ToDateTime(eventData[4]),
                        DateEnd = Convert.ToDateTime(eventData[5]),
                        Description = eventData[6]
                    };
                    _context.Events.Add(ev);
                    _context.SaveChanges();
                

                      i += 10;
                }
            }
        }
    }

}
