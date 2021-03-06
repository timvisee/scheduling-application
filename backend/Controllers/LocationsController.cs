﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private async Task<ApplicationUser> GetUser() => await _userManager.FindByNameAsync(User.Identity.Name);
        // Does not work with a variable, need to be a method
        private Role GetRole() => GetUser().Result.User.Role;

        public LocationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Locations
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.UserCanEdit = GetRole() == Role.Admin || GetRole() == Role.Elevated;

            var locations = from e in _context.Locations select e;
            locations = locations.OrderBy(e => e.Name);

            return View(locations);
        }

        // GET: Locations/Details/5
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC, READONLY")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .SingleOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Latitude,Longitude")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Latitude,Longitude")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .SingleOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }

        /**
        * Seeds database with Location objects
        */
        public IActionResult Seed()
        {
            var locations = 5;

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

            return RedirectToAction("Index");
        }

        /**
         * Delete all Location entries
         */
        public IActionResult DeleteAll()
        {
            _context.Locations.Clear();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
