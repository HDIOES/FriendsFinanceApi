using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriendsFinanceApi.Repository;
using FriendsFinanceApi.Repository.Models;

namespace FriendsFinanceApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly DataContext _context;

        private async Task<int> getCurrentUserId()
        {
            return 1;
        }

        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivitys()
        {
            if (_context.Activitys == null)
            {
                return NotFound();
            }
            var userId = await getCurrentUserId();
            return await _context.Activitys.Where(x => x.OwnerId == userId).ToListAsync();
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            if (_context.Activitys == null)
            {
                return NotFound();
            }
            var activity = await _context.Activitys.FindAsync(id);
            
            if (activity == null)
            {
                return NotFound();
            }
            activity.Members = await _context.ActivityMembers.Where(x => x.ActivityId == id).ToListAsync();
            return activity;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {
            if (_context.Activitys == null)
            {
                return Problem("Entity set 'DataContext.Activitys'  is null.");
            }
            activity.OwnerId = await getCurrentUserId();
            activity.Owner = null;
            activity.Members = null;
            _context.Activitys.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            if (_context.Activitys == null)
            {
                return NotFound();
            }
            var userId = await getCurrentUserId();
            var activity = await _context.Activitys.SingleOrDefaultAsync(x => x.Id == id && x.OwnerId == userId);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activitys.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(int id)
        {
            return (_context.Activitys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
