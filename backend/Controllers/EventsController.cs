using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Extensions;
using backend.Models;

namespace backend.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public IActionResult Index()
        {
            var events = from e in _context.Events select e;
            events = events.OrderBy(e => e.Start);

            return View(events);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            //get all linked attendees
            var attendeeIds = _context.EventAttendees.Where(x => x.PeopleId == id).Select(x => x.PeopleId);

            var attendees = _context.People.Where(x => attendeeIds.Contains(x.Id)).ToList();
            ViewBag.Attendees = attendees.Count > 0 ? attendees : null;

            //get all linked owners
            var ownerIds = _context.EventOwners.Where(x => x.PeopleId == id).Select(x => x.PeopleId);

            var owners = _context.People.Where(x => ownerIds.Contains(x.Id)).ToList();
            ViewBag.Owners = owners.Count > 0 ? owners : null;

            //get all linked groups
            var groupIds = _context.PeopleGroups.Where(x => x.PeopleId == id).Select(x => x.GroupId);

            var groups = _context.People.Where(x => groupIds.Contains(x.Id)).ToList();
            ViewBag.Groups = groups.Count > 0 ? groups : null;
            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewBag.Owners = new MultiSelectList(_context.People, "Id", "TypedDisplayName");
            ViewBag.Attendees = new MultiSelectList(_context.People, "Id", "TypedDisplayName");
            ViewBag.Locations = new MultiSelectList(_context.Locations, "Id", "Name");

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
                List<int> owners,
                List<int> attendees,
                List<int> locations,
                [Bind("Id,Title,Description,Start,End,Owners,Attendees,Locations")] Event @event
        ) {
            if (ModelState.IsValid)
            {
                _context.Add(@event);

                await _context.SaveChangesAsync();

                // Add the new couplings
                foreach (var peopleId in owners)
                {
                    @event.Owners.Add(
                        new EventOwner {
                            EventId = @event.Id,
                            PeopleId = peopleId
                        }
                    );
                }
                foreach (var peopleId in attendees)
                {
                    @event.Attendees.Add(
                        new EventAttendee {
                            EventId = @event.Id,
                            PeopleId = peopleId
                        }
                    );
                }
                foreach (var locationId in locations)
                {
                    @event.Locations.Add(
                        new EventLocation {
                            EventId = @event.Id,
                            LocationId = locationId
                        }
                    );
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            // Get the owner and attendee IDs
            var ownerIds = _context.EventOwners
                .Where(e => e.EventId == id)
                .Select(e => e.PeopleId)
                .ToList();
            var attendeeIds = _context.EventAttendees
                .Where(e => e.EventId == id)
                .Select(e => e.PeopleId)
                .ToList();
            var locationIds = _context.EventLocations
                .Where(e => e.EventId == id)
                .Select(e => e.LocationId)
                .ToList();

            // Build the multi select lists
            ViewBag.Owners = new MultiSelectList(
                _context.People,
                "Id",
                "TypedDisplayName",
                ownerIds
            );
            ViewBag.Attendees = new MultiSelectList(
                _context.People,
                "Id",
                "TypedDisplayName",
                attendeeIds
            );
            ViewBag.Locations = new MultiSelectList(
                _context.Locations,
                "Id",
                "Name",
                locationIds
            );

            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
                int id,
                List<int> owners,
                List<int> attendees,
                List<int> locations,
                [Bind("Id,Title,Description,Start,End,Owners,Attendees,Locations")] Event @event
        ) {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);

                    // Remove all previous couplings
                    _context.RemoveRange(
                        _context.EventOwners.Where(e => e.EventId == id)
                    );
                    _context.RemoveRange(
                        _context.EventAttendees.Where(e => e.EventId == id)
                    );
                    _context.RemoveRange(
                        _context.EventLocations.Where(e => e.EventId == id)
                    );
                    _context.SaveChanges();

                    // Add the new couplings
                    foreach (var peopleId in owners)
                    {
                        @event.Owners.Add(
                            new EventOwner {
                                EventId = @event.Id,
                                PeopleId = peopleId
                            }
                        );
                    }
                    foreach (var peopleId in attendees)
                    {
                        @event.Attendees.Add(
                            new EventAttendee {
                                EventId = @event.Id,
                                PeopleId = peopleId
                            }
                        );
                    }
                    foreach (var locationId in locations)
                    {
                        @event.Locations.Add(
                            new EventLocation {
                                EventId = @event.Id,
                                LocationId = locationId
                            }
                        );
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        /**
        * Seeds database with Event objects
        */
        public IActionResult Seed()
        {
            DateTime start = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);

            for (int i = 1; i < 15; i++)
            {
                var ev = new Event
                {
                    Start = new DateTime(2018, start.Month, start.Day, 8 + i, 0, 0, 0),
                    End = new DateTime(2018, start.Month, start.Day, 9 + i, 0, 0, 0),
                    Description = "This is a description where we describe with a describing description",
                    Title = "Title of Event",
                };
                _context.Events.Add(ev);
                if (i % 3 == 0 && i > 0)
                {
                    start = start.AddDays(1);

                }
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        /**
         * Delete all Event entries
         */
        public IActionResult DeleteAll()
        {
            _context.Events.Clear();
            _context.EventAttendees.Clear();
            _context.EventOwners.Clear();
            _context.EventLocations.Clear();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult JsonDetails(int id)
        {
            var eve = _context.Events.Find(id);

            return Json(new EventInformation(eve));
        }
    }
}
