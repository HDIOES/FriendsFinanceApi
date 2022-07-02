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
    public class ActivityMembersController : ControllerBase
    {
        private readonly DataContext _context;

        public ActivityMembersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ActivityMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityMember>>> GetActivityMembers()
        {
          if (_context.ActivityMembers == null)
          {
              return NotFound();
          }
            return await _context.ActivityMembers.ToListAsync();
        }

        // GET: api/ActivityMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityMember>> GetActivityMember(int id)
        {
          if (_context.ActivityMembers == null)
          {
              return NotFound();
          }
            var activityMember = await _context.ActivityMembers.FindAsync(id);

            if (activityMember == null)
            {
                return NotFound();
            }

            return activityMember;
        }

        // PUT: api/ActivityMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityMember(int id, ActivityMember activityMember)
        {
            if (id != activityMember.Id)
            {
                return BadRequest();
            }

            _context.Entry(activityMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityMemberExists(id))
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

        // POST: api/ActivityMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityMember>> PostActivityMember(ActivityMember activityMember)
        {
          if (_context.ActivityMembers == null)
          {
              return Problem("Entity set 'DataContext.ActivityMembers'  is null.");
          }
            activityMember.User = null;
            activityMember.Activity = null;
            _context.ActivityMembers.Add(activityMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityMember", new { id = activityMember.Id }, activityMember);
        }

        // DELETE: api/ActivityMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityMember(int id)
        {
            if (_context.ActivityMembers == null)
            {
                return NotFound();
            }
            var activityMember = await _context.ActivityMembers.FindAsync(id);
            if (activityMember == null)
            {
                return NotFound();
            }

            _context.ActivityMembers.Remove(activityMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityMemberExists(int id)
        {
            return (_context.ActivityMembers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
