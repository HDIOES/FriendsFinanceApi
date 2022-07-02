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
    public class FriendsController : ControllerBase
    {
        private readonly DataContext _context;

        private async Task<int> getCurrentUserId()
        {
            return 1;
        }

        public FriendsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friends>>> GetFriends()
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            return await _context.Friends.ToListAsync();
        }

        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> GetFriends(int id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            var friends = await _context.Friends.FindAsync(id);

            if (friends == null)
            {
                return NotFound();
            }

            return friends;
        }

        // POST: api/Friends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friends>> PostFriends(Friends friends)
        {
            if (_context.Friends == null)
            {
                return Problem("Entity set 'DataContext.Friends'  is null.");
            }
            _context.Friends.Add(friends);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriends", new { id = friends.Id }, friends);
        }

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriends(int id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            var friends = await _context.Friends.FindAsync(id);
            if (friends == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friends);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendsExists(int id)
        {
            return (_context.Friends?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
