using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp
{
    public class EventController : Controller
    {
        private readonly DbEntity _context;

        public EventController(DbEntity context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            return Json(await _context.Events.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var even = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            if (even == null)
            {
                return NotFound();
            }

            return Json(even);
        }

        /* // GET: User/Create */
        /* public IActionResult Create() */
        /* { */
        /*     return View(); */
        /* } */

        /* // POST: User/Create */
        /* // To protect from overposting attacks, please enable the specific properties you want to bind to, for */
        /* // more details see http://go.microsoft.com/fwlink/?LinkId=317598. */
        /* [HttpPost] */
        /* [ValidateAntiForgeryToken] */
        /* public async Task<IActionResult> Create([Bind("UserId,Participant,FirstName,Infix,LastName,Locale,Type,Role,Deleted")] User user) */
        /* { */
        /*     if (ModelState.IsValid) */
        /*     { */
        /*         _context.Add(user); */
        /*         await _context.SaveChangesAsync(); */
        /*         return RedirectToAction(nameof(Index)); */
        /*     } */
        /*     return View(user); */
        /* } */

        /* // GET: User/Edit/5 */
        /* public async Task<IActionResult> Edit(int? id) */
        /* { */
        /*     if (id == null) */
        /*     { */
        /*         return NotFound(); */
        /*     } */

        /*     var user = await _context.Users.SingleOrDefaultAsync(m => m.UserId == id); */
        /*     if (user == null) */
        /*     { */
        /*         return NotFound(); */
        /*     } */
        /*     return View(user); */
        /* } */

        /* // POST: User/Edit/5 */
        /* // To protect from overposting attacks, please enable the specific properties you want to bind to, for */
        /* // more details see http://go.microsoft.com/fwlink/?LinkId=317598. */
        /* [HttpPost] */
        /* [ValidateAntiForgeryToken] */
        /* public async Task<IActionResult> Edit(int id, [Bind("UserId,Participant,FirstName,Infix,LastName,Locale,Type,Role,Deleted")] User user) */
        /* { */
        /*     if (id != user.UserId) */
        /*     { */
        /*         return NotFound(); */
        /*     } */

        /*     if (ModelState.IsValid) */
        /*     { */
        /*         try */
        /*         { */
        /*             _context.Update(user); */
        /*             await _context.SaveChangesAsync(); */
        /*         } */
        /*         catch (DbUpdateConcurrencyException) */
        /*         { */
        /*             if (!UserExists(user.UserId)) */
        /*             { */
        /*                 return NotFound(); */
        /*             } */
        /*             else */
        /*             { */
        /*                 throw; */
        /*             } */
        /*         } */
        /*         return RedirectToAction(nameof(Index)); */
        /*     } */
        /*     return View(user); */
        /* } */

        /* // GET: User/Delete/5 */
        /* public async Task<IActionResult> Delete(int? id) */
        /* { */
        /*     if (id == null) */
        /*     { */
        /*         return NotFound(); */
        /*     } */

        /*     var user = await _context.Users */
        /*         .SingleOrDefaultAsync(m => m.UserId == id); */
        /*     if (user == null) */
        /*     { */
        /*         return NotFound(); */
        /*     } */

        /*     return View(user); */
        /* } */

        /* // POST: User/Delete/5 */
        /* [HttpPost, ActionName("Delete")] */
        /* [ValidateAntiForgeryToken] */
        /* public async Task<IActionResult> DeleteConfirmed(int id) */
        /* { */
        /*     var user = await _context.Users.SingleOrDefaultAsync(m => m.UserId == id); */
        /*     _context.Users.Remove(user); */
        /*     await _context.SaveChangesAsync(); */
        /*     return RedirectToAction(nameof(Index)); */
        /* } */

        /* private bool UserExists(int id) */
        /* { */
        /*     return _context.Users.Any(e => e.UserId == id); */
        /* } */
    }
}
