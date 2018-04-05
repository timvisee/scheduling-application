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
        public IActionResult Index()
        {
            return Redirect("/People");
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var @group = await _context.Groups
                .SingleOrDefaultAsync(m => m.Id == id);

            if (@group == null)
                return NotFound();


            //get all related events
            var relatedEvents = _context.EventAttendees.Where(x => x.PeopleId == id).Select(x => x.EventId);
            var CoupledEvents = _context.Events.Where(x => relatedEvents.Contains(x.Id)).ToList();

            ViewBag.CoupledEvents = CoupledEvents.Count > 0 ? CoupledEvents : null;
            return View(@group);
        }

        // GET: Groups/Create
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

                foreach (var PeopleId in People)
                {
                    @group.People.Add(
                        new PeopleGroup {
                        GroupId = @group.Id,
                        PeopleId = PeopleId
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", nameof(People));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
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

            //get all UserGroup connections linked to this group
            var connections = _context.PeopleGroups.Where(x => x.GroupId == id).Select(x => x.PeopleId);

            //get all groups + linked ones to this group
            ViewBag.ConnectedPeople = new MultiSelectList(
                _context.People.Where(x => x.Id != id).ToList(),
                "Id", "TypedDisplayName",
                _context.People.Where(x => connections.Contains(x.Id)).ToList().Select(x => x.Id)
            );


            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, List <int> People, [Bind("Id,Name,People")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.RemoveRange(_context.PeopleGroups.Where(e => e.GroupId == id));
                    _context.Update(group);
                    _context.SaveChanges();

                    foreach (var PeopleId in People)
                    {
                        @group.People.Add(
                            new PeopleGroup {
                                GroupId = @group.Id,
                                PeopleId = PeopleId
                            });
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", nameof(People));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", nameof(People));
            }
            return View(@group);
        }

        // GET: Groups/Delete/5
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
    }
}
