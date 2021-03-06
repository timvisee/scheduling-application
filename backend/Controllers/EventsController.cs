﻿using System;
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
using backend.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace backend.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private async Task<ApplicationUser> GetUser() => await _userManager.FindByNameAsync(User.Identity.Name);

        // Does not work with a variable, need to be a method
        private Role GetRole() => GetUser().Result.User.Role;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Events
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC,READONLY")]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.UserCanEdit = GetRole() == Role.Admin || GetRole() == Role.Elevated;

            var enrolledEvents = GetAllEnrolledEvents(GetUser().Result.User.Id);
            ViewBag.EnrolledEvents = enrolledEvents;

            ViewBag.AllEvents = _context.Events.Where(e => !enrolledEvents.Contains(e)).ToList();


            return View();
        }

        // GET: Events/Details/5
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC,READONLY")]
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

            //send user role to details (to check for editing rights)
            ViewBag.UserCanEdit = GetRole() == Role.Admin || GetRole() == Role.Elevated;

            //get all linked attendees
            var attendeeIds = _context.EventAttendees.Where(x => x.EventId == id).Select(x => x.PeopleId);

            var attendees = _context.People.Where(x => attendeeIds.Contains(x.Id)).ToList();
            ViewBag.Attendees = attendees.Count > 0 ? attendees : null;

            //get all linked owners
            var ownerIds = _context.EventOwners.Where(x => x.EventId == id).Select(x => x.PeopleId);

            var owners = _context.People.Where(x => ownerIds.Contains(x.Id)).ToList();
            ViewBag.Owners = owners.Count > 0 ? owners : null;

            //get all linked locations
            var locationIds = _context.EventLocations.Where(x => x.EventId == id).Select(x => x.LocationId);

            var locations = _context.Locations.Where(x => locationIds.Contains(x.Id)).ToList();
            ViewBag.Locations = locations.Count > 0 ? locations : null;

            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
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
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
        public async Task<IActionResult> Create(
            List<int> owners,
            List<int> attendees,
            List<int> locations,
            [Bind("Id,Title,Description,Start,End,Owners,Attendees,Locations")]
            Event @event
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);

                await _context.SaveChangesAsync();

                // Add the new couplings
                foreach (var peopleId in owners)
                {
                    @event.Owners.Add(
                        new EventOwner
                        {
                            EventId = @event.Id,
                            PeopleId = peopleId
                        }
                    );
                }

                foreach (var peopleId in attendees)
                {
                    @event.Attendees.Add(
                        new EventAttendee
                        {
                            EventId = @event.Id,
                            PeopleId = peopleId
                        }
                    );
                }

                foreach (var locationId in locations)
                {
                    @event.Locations.Add(
                        new EventLocation
                        {
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
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
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

            var ev = new EventView();
            ev.Event = @event;
            ev.SelectedAttendees = _context.EventAttendees
                .Where(e => e.EventId == id)
                .Select(e => e.PeopleId)
                .ToList();

            // Get the owner and attendee IDs
            ev.SelectedOwners = _context.EventOwners
                .Where(e => e.EventId == id)
                .Select(e => e.PeopleId)
                .ToList();

            ev.SelectedLocations = _context.EventLocations
                .Where(e => e.EventId == id)
                .Select(e => e.LocationId)
                .ToList();

            ev.Attendees = _context.People.ToList();
            ev.Owners = _context.People.ToList();
            ev.Locations = _context.Locations.ToList();

            ev.AttendeeList = new SelectList(_context.People, "Id", "TypedDisplayName");
            ev.OwnerList = new SelectList(_context.People, "Id", "TypedDisplayName");
            ev.LocationList = new SelectList(_context.Locations, "Id", "Name");

            return View(ev);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
        public IActionResult Edit(int id, EventView updatedEvent)
        {
            if (id != updatedEvent.Event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // First remove all the current couplings between this event and locations, attendees and owners
                var rml = _context.EventLocations.Where(el => el.EventId == id);
                var rma = _context.EventAttendees.Where(ea => ea.EventId == id);
                var rmo = _context.EventOwners.Where(eo => eo.EventId == id);
                _context.RemoveRange(rml);
                _context.RemoveRange(rma);
                _context.RemoveRange(rmo);

                _context.SaveChanges();


                // Add locations, attendees and owners
                if (updatedEvent.SelectedLocations != null)
                {
                    foreach (var locationId in updatedEvent.SelectedLocations)
                    {
                        _context.EventLocations.Add(new EventLocation
                        {
                            EventId = updatedEvent.Event.Id,
                            LocationId = locationId
                        });
                    }
                }

                if (updatedEvent.SelectedAttendees != null)
                {
                    foreach (var attendeeId in updatedEvent.SelectedAttendees)
                    {
                        _context.EventAttendees.Add(new EventAttendee
                        {
                            EventId = updatedEvent.Event.Id,
                            PeopleId = attendeeId
                        });
                    }
                }

                if (updatedEvent.SelectedOwners != null)
                {
                    foreach (var ownerId in updatedEvent.SelectedOwners)
                    {
                        _context.EventOwners.Add(new EventOwner
                        {
                            EventId = updatedEvent.Event.Id,
                            PeopleId = ownerId
                        });
                    }
                }

                _context.Events.Update(updatedEvent.Event);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedEvent);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
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
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
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



        [HttpGet]
        public JsonResult JsonList()
        {

            var allEvents = GetAllEnrolledEvents(GetUser().Result.User.Id);
            //Distincts all events, so no duplicates and return a CalendarEvent instance
            List<CalendarEvent> ce = allEvents.GroupBy(x => x.Id)
                .Select(g => g.First())
                .Select(ev => new CalendarEvent(ev))
                .ToList();

            return Json(ce);
        }

        public IActionResult Enroll(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            _context.EventAttendees.Add(new EventAttendee
            {
                EventId = id,
                PeopleId = GetUser().Result.User.Id
            });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Leave(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var userId = GetUser().Result.User.Id;

            //check if user has a link to the event
            var userIsOwner = _context.EventOwners.Any(x => x.PeopleId == userId && x.EventId == id);
            var userIsAttendee = _context.EventAttendees.Any(x => x.PeopleId == userId && x.EventId == id);

            if (userIsOwner)
            {
                _context.EventOwners.Remove(_context.EventOwners.First(x => x.PeopleId == userId && x.EventId == id));
                _context.SaveChanges();
            }

            if (userIsAttendee)
            {
                _context.EventAttendees.Remove(_context.EventAttendees.First(x => x.PeopleId == userId && x.EventId == id));
                _context.SaveChanges();
            }

            if (!userIsAttendee && !userIsOwner)
            {
                TempData["alertMessage"] =
                    "You are enrolled to a group which is connected to this event. If you want to leave this event you have to leave the group.";
            }

            return RedirectToAction("Index");
        }

        public List<Event> GetAllEnrolledEvents(int userId)
        {

            List <Event> allEvents = new List<Event>();

            //get all attended events of user
            var attendeeEvents = _context.EventAttendees.Where(
                x => x.PeopleId == userId)
                .Select(x => x.EventId);

            allEvents.AddRange(_context.Events.Where(
                x => attendeeEvents.Contains(x.Id)));

            //get all events where the userId is the owner
            var ownerEvents = _context.EventOwners.Where(
                x => x.PeopleId == userId)
                .Select(x => x.EventId);

            allEvents.AddRange(_context.Events.Where(
                x => ownerEvents.Contains(x.Id)));

            // Get all linked groups
            var linkedGroups = _context.PeopleGroups.Where(
                x => x.PeopleId == userId)
                .Select(x => x.GroupId);

            var groups = _context.Groups.Where(
                x => linkedGroups.Contains(x.Id));

            //get group events (no deep group events)
            foreach (var @group in groups)
            {
                var ownerIds = _context.EventOwners.Where(
                    x => x.PeopleId == group.Id)
                    .Select(x => x.EventId);
                allEvents.AddRange(_context.Events.Where(
                    x => ownerIds.Contains(x.Id)));

                var attendeeIds = _context.EventAttendees.Where(
                        x => x.PeopleId == group.Id)
                    .Select(x => x.EventId);
                allEvents.AddRange(_context.Events.Where(
                    x => attendeeIds.Contains(x.Id)));

                var deepGroups = group.AllGroups();

                //get all deep groups
                foreach (var deepGroup in deepGroups)
                {
                    var deepOwnerIds = _context.EventOwners.Where(
                            x => x.PeopleId == deepGroup.Id)
                        .Select(x => x.EventId);
                    allEvents.AddRange(_context.Events.Where(
                        x => deepOwnerIds.Contains(x.Id)));

                    var deepAttendeeIds = _context.EventAttendees.Where(
                            x => x.PeopleId == deepGroup.Id)
                        .Select(x => x.EventId);
                    allEvents.AddRange(_context.Events.Where(
                        x => deepAttendeeIds.Contains(x.Id)));
                }
            }
            return allEvents;
        }
    }
}
