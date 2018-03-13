using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetch a list of all people.
        /// </summary>
        ///
        /// <returns>
        /// JSON array of peoples.
        /// </returns>
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Json(await _context.Peoples.ToListAsync());
        }

        /// <summary>
        /// Fetch details of a specific people with the given id.
        /// </summary>
        ///
        /// <returns>
        /// JSON object of the people.
        /// </returns>
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var even = await _context.Peoples.SingleOrDefaultAsync(m => m.PeopleId == id);
            if (even == null)
            {
                return NotFound();
            }

            return Json(even);
        }
    }
}
