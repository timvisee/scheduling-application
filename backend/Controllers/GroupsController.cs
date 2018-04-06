using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace backend.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private async Task<ApplicationUser> GetUser() => await _userManager.FindByNameAsync(User.Identity.Name);

        // Does not work with a variable, need to be a method
        private Role GetRole() => GetUser().Result.User.Role;

        public GroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Groups
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            //get all enrolled groups for user
            var enrolledIds = _context.PeopleGroups.Where(x => x.PeopleId == GetUser().Result.User.Id)
                .Select(x => x.GroupId);
            ViewBag.EnrolledGroups = _context.Groups.Where(x => enrolledIds.Contains(x.Id)).ToList();

            //get all leftover groups
            ViewBag.LeftOvers = _context.Groups.Where(x => !enrolledIds.Contains(x.Id)).ToList();

            return View(_context.Groups.ToList());
        }

        // GET: Groups/Details/5
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var @group = await _context.Groups
                .SingleOrDefaultAsync(m => m.Id == id);

            if (@group == null)
                return NotFound();

            ViewBag.UserCanEdit = GetRole() == Role.Admin || GetRole() == Role.Elevated;

            //get all related events
            var relatedEvents = _context.EventAttendees.Where(x => x.PeopleId == id).Select(x => x.EventId);
            var CoupledEvents = _context.Events.Where(x => relatedEvents.Contains(x.Id)).ToList();

            ViewBag.CoupledEvents = CoupledEvents.Count > 0 ? CoupledEvents : null;
            return View(@group);
        }

        // GET: Groups/Create
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public IActionResult Create()
        {
            ViewBag.People = new MultiSelectList(_context.People, "Id", "TypedDisplayName");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> Create(List<int> People, [Bind("Id,Name,People")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.RemoveRange(
                    _context.PeopleGroups.Where(e => e.GroupId == group.Id)
                );

                _context.SaveChanges();

                _context.Add(@group);

                _context.SaveChanges();

                foreach (var peopleId in People)
                {
                    @group.People.Add(
                        new PeopleGroup
                        {
                            GroupId = @group.Id,
                            PeopleId = peopleId
                        });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", nameof(People));
            }

            return View(@group);
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            var gv = new GroupView();
            gv.group = @group;
            gv.SelectedPeople = _context.PeopleGroups
                .Where(g => g.GroupId == id)
                .Select(g => g.PeopleId)
                .ToList();
            gv.People = _context.People.ToList();
            gv.PeopleList = new SelectList(_context.People.Where(g => g.Id != id), "Id", "TypedDisplayName");

            return View(gv);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public IActionResult Edit(int id, GroupView updatedGroup)
        {
            if (id != updatedGroup.group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var rmp = _context.PeopleGroups.Where(eo => eo.GroupId == id);
                _context.RemoveRange(rmp);
                _context.SaveChanges();

                // Add locations, attendees and owners
                if (updatedGroup.SelectedPeople != null)
                {
                    foreach (var peopleId in updatedGroup.SelectedPeople)
                    {
                        _context.PeopleGroups.Add(new PeopleGroup
                        {
                            GroupId = updatedGroup.group.Id,
                            PeopleId = peopleId
                        });
                    }
                }

                _context.Groups.Update(updatedGroup.group);
                _context.SaveChanges();

                return RedirectToAction("Index", nameof(People));
            }

            return View(updatedGroup);
        }

        // GET: Groups/Delete/5
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,ELEVATED")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", nameof(People));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }

        /**
         * Leave a group which a user is enrolled for
         */
        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
        public IActionResult Leave(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            _context.PeopleGroups.RemoveRange(
                _context.PeopleGroups.Where(
                    x => x.GroupId == id &&
                         x.PeopleId == GetUser().Result.User.Id
                ));

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN,ELEVATED,BASIC")]
        public IActionResult Enroll(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            _context.PeopleGroups.Add(new PeopleGroup
            {
                GroupId = id,
                PeopleId = GetUser().Result.User.Id
            });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
