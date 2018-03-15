using System;
using System.Collections.Generic;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Calendar()
        {
            return View();
        }




       
        

            /* > Laden van data uit iCal.
               > Data uit ical gebruiken om nieuwe events models te maken.
               > model omzetten naar jsonresult.
               ical.fromURL(url, options, function(err, data) {} )
ical.fromURL('http://ruz.spbstu.ru/faculty/100/groups/25242/ical?date=2018-3-12', { }, function(err, data)

             */

            public JsonResult GetEvents(double start, double end)

            {

            
            

            }
            
        }

    }
}
