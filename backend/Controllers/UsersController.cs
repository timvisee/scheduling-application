using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.App.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;

namespace backend.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private async Task<ApplicationUser> GetUser() => await _userManager.FindByNameAsync(User.Identity.Name);

        // Does not work with a variable, need to be a method
        private Role GetRole() => GetUser().Result.User.Role;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public IActionResult Index()
        {
            if (GetRole() == Role.Elevated || GetRole() == Role.Admin)
            {
                return RedirectToAction("Index", "People");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (GetRole() != Role.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new UserView());
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserView newUser)
        {
            if (GetRole() != Role.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                _context.Users.Add(newUser.User);
                _context.SaveChanges();

                // Create new applicationuser
                var appUser = new ApplicationUser
                {
                    Email = newUser.Email,
                    NormalizedEmail = newUser.Email.ToUpper(),
                    UserName = newUser.Email,
                    NormalizedUserName = newUser.Email.ToUpper(),
                    PhoneNumber = newUser.Phone,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    User = newUser.User
                };

                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(appUser, newUser.Password);
                appUser.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(_context);
                var result = userStore.CreateAsync(appUser);

                // Wait for the actual result
                result.Wait();

                return RedirectToAction("Index", nameof(People));
            }

            return View(newUser);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (GetRole() != Role.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            var appUser = _context.ApplicationUsers.FirstOrDefault(u => u.UserID == id);

            if (user == null || appUser == null)
            {
                return NotFound();
            }

            var uv = new UserView
            {
                User = user,
                Email = appUser.Email,
                Phone = appUser.PhoneNumber
            };

            return View(uv);
        }

        /**
         * POST for user edit screen
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserView updatedUser)
        {
            if (GetRole() != Role.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id != updatedUser.User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedUser.User);
                    _context.SaveChanges();

                    var appUser = _context.ApplicationUsers.FirstOrDefault(u => u.UserID == updatedUser.User.Id);
                    if (appUser != null)
                    {
                        appUser.Email = updatedUser.Email;
                        appUser.NormalizedEmail = updatedUser.Email;
                        appUser.PhoneNumber = updatedUser.Phone;

                        _context.ApplicationUsers.Update(appUser);
                        _context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("ApplicationUser could not be found.");
                    }
                }
                catch (Exception)
                {
                    if (!UserExists(updatedUser.User.Id))
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


            return View(new UserView());
        }


        /**
         * Remove functionality
         */
        public async Task<IActionResult> Delete(int? id)
        {
            if (GetRole() != Role.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (GetRole() != Role.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            var @user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(@user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", nameof(People));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
