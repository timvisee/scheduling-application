using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    public class EventLocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventLocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventLocation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventLocations.Include(e => e.Event).Include(e => e.Location);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EventLocation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocations
                .Include(e => e.Event)
                .Include(e => e.Location)
                .SingleOrDefaultAsync(m => m.EventId == id);
            if (eventLocation == null)
            {
                return NotFound();
            }

            return View(eventLocation);
        }

        // GET: EventLocation/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: EventLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,LocationId")] EventLocation eventLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", eventLocation.EventId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", eventLocation.LocationId);
            return View(eventLocation);
        }

        // GET: EventLocation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocations.SingleOrDefaultAsync(m => m.EventId == id);
            if (eventLocation == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", eventLocation.EventId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", eventLocation.LocationId);
            return View(eventLocation);
        }

        // POST: EventLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,LocationId")] EventLocation eventLocation)
        {
            if (id != eventLocation.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventLocationExists(eventLocation.EventId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", eventLocation.EventId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", eventLocation.LocationId);
            return View(eventLocation);
        }

        // GET: EventLocation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocations
                .Include(e => e.Event)
                .Include(e => e.Location)
                .SingleOrDefaultAsync(m => m.EventId == id);
            if (eventLocation == null)
            {
                return NotFound();
            }

            return View(eventLocation);
        }

        // POST: EventLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventLocation = await _context.EventLocations.SingleOrDefaultAsync(m => m.EventId == id);
            _context.EventLocations.Remove(eventLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventLocationExists(int id)
        {
            return _context.EventLocations.Any(e => e.EventId == id);
        }
    }
}
